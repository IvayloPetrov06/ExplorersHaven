﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.Xml;
using System.Threading.Channels;
using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Main;
using Explorers_Haven.ViewModels.Offer;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Options;
using Mono.TextTemplating;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Xml.Linq;

namespace Explorers_Haven.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IOfferService _offerService;
        private readonly IAmenityService _amenityService;
        private readonly IActivityService _activityService;
        private readonly ITravelService _travelService;
        private readonly IStayService _stayService;
        private readonly IRatingService _ratingService;
        private readonly ICommentService _commentService;
        private readonly IFavoriteService _favoriteService;
        private readonly ITransportService _transportService;
        private readonly IBookingService _bookingService;
        private readonly IStayAmenityService _StayAmenityService;
        IUserService userService;


        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public HomeController(IStayAmenityService StayAmenityService, IBookingService bookingService, IFavoriteService favoriteService, ITransportService transportService, UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IActivityService activyService, ITravelService travelService, ICommentService commentService, IAmenityService amenityService, IRatingService ratingService, IStayService stayService, IOfferService offerService, IUserService userService)
        {
            _StayAmenityService = StayAmenityService;
            _bookingService = bookingService;
            _transportService = transportService;
            _favoriteService = favoriteService;
            _offerService = offerService;
            this.userService = userService;
            _activityService = activyService;
            _travelService = travelService;
            _amenityService = amenityService;
            _stayService = stayService;
            _ratingService = ratingService;
            _commentService = commentService;

            userManager = _userManager;

            this.cloudService = cloud;

            _configuration = configuration;
            var account = new Account(
           _configuration["Cloudinary:CloudName"],
           _configuration["Cloudinary:ApiKey"],
           _configuration["Cloudinary:ApiSecret"]
             );
            _cloudinary = new Cloudinary(account);
        }
        public async Task<IActionResult> HomePage(HomePageViewModel filter)
        {
            var query = _offerService.GetAll().AsQueryable();
            var filterModel = new OfferFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {

                var model = _offerService.CombinedInclude().Include(x => x.User).Select(x => new OfferViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    CoverImage = x.CoverImage,
                    UserName = x.User.Username
                }).ToList();

                filterModel = new OfferFilterViewModel
                {
                    Offers = model,
                    Search = filter.Search,

                };
            }
            else
            {
                var tempUsers = await userService.GetAllUserNamesAsync();
                var tempOffers = await _offerService.GetAllOfferNamesAsync();
                if (tempUsers.Contains(filter.Search))
                {
                    query = query.Where(x => x.User.Username == filter.Search);
                }
                if (tempOffers.Contains(filter.Search))
                {
                    query = query.Where(x => x.Name == filter.Search);
                }

                filterModel = new OfferFilterViewModel
                {
                    Offers = query.Include(x => x.User)
                .Select(x => new OfferViewModel()
                {
                    Name = x.Name,
                    CoverImage = x.CoverImage,
                    Price = x.Price,
                    Id = x.Id,
                    UserName = x.User.Username
                }).ToList(),
                    Search = filter.Search
                };
            };
            var sortedList = query.OrderBy(x => x.Price).ToList();
            filterModel.Cheapest_Offers = sortedList;

            return View(filterModel);
        }
        public async Task<IActionResult> OfferPage(int id)
        {
            //isBook
            var tempOffer = await _offerService.GetOfferByIdAsync(id);
            var commUsers = await userService.GetAllUsersAsync();
            var tempStay = await _stayService.GetStayByIdAsync(tempOffer.StayId.Value);
            var transports = await _transportService.GetAllTransportsAsync();

            var model = _offerService.GetAll().Where(x => x.Id == id).Include(x => x.User)
            .Select(x => new OfferPageViewModel()
            {
                OfferDiscount = x.Discount,
                OfferPeople = x.MaxPeople,
                OfferDays = x.DurationDays,
                OfferStart = x.StartDate,
                OfferLast = x.LastDate,
                OfferId = tempOffer.Id,
                OfferPic = x.BackImage,
                OfferPrice = x.Price,
                OfferName = x.Name,
                OfferDisc = x.Disc,
                StayName = tempStay.Name,
                StayPic = tempStay.Image,
                StayPrice = tempStay.Price,
                StayDisc = tempStay.Disc,
                StayStars = tempStay.Stars,
                Users = commUsers.ToList(),
                UserId = x.UserId,
                Transports = transports.ToList(),
            }).FirstOrDefault();

            if (tempOffer.Discount != null)
            {
                model.IsOnDiscount = true;
            }
            else
            {
                model.IsOnDiscount = false;
            }
            var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
            User userModel = await userService.GetUserAsync(x => x.Email == tempUser.Email);
            var fav = await _favoriteService.GetFavoriteAsync(x => x.OfferId == tempOffer.Id && x.UserId == userModel.Id);
            if (fav == null)
            {
                model.IsFavorited = false;
            }
            else
            {
                model.IsFavorited = true;
            }
            var bo = await _bookingService.GetBookingAsync(x => x.OfferId == tempOffer.Id && x.UserId == userModel.Id);
            if (bo == null)
            {
                model.IsBooked = false;
            }
            else
            {
                model.IsBooked = true;
            }

            var tempCom = await _commentService.GetAllCommentsAsync(x => x.OfferId == id);
            model.Comments = tempCom.ToList();

            if (tempCom.Count() != 0)//ako ima reviewta
            {
                decimal rates = 0;
                foreach (var r in tempCom)
                {
                    rates += r.Stars;
                }
                decimal AverageRate;
                decimal ofst;
                if (tempOffer.Rating != null)//ako ofertata ima default rating
                {
                    rates = +tempOffer.Rating.Value;
                    int countt = tempCom.Count() + 1;
                    AverageRate = rates / countt;
                    ofst = Math.Round(AverageRate, 2);
                }
                else//ako nqma
                {
                    AverageRate = rates / tempCom.Count();
                    ofst = Math.Round(AverageRate, 2);
                }
                model.OfferRating = AverageRate;
                model.OfferRatingStars = ofst;
            }
            else//ako nqma reviewta
            {
                if (tempOffer.Rating != null)
                {
                    model.OfferRatingStars = Math.Round(tempOffer.Rating.Value,2);
                    model.OfferRating = tempOffer.Rating;
                }
                else
                {
                    model.OfferRatingStars = 3;
                    model.OfferRating = 3;
                }

            }
            var tempActivities = _activityService.GetAll().ToList();
            foreach (var ac in tempActivities)
            {
                if (ac.OfferId == tempOffer.Id)
                {
                    model.Activities.Add(ac);
                }

            }
            var tempTravels = _travelService.GetAll();
            foreach (var ac in tempTravels)
            {
                if (ac.OfferId == tempOffer.Id)
                {
                    model.Travels.Add(ac);
                }

            }
            var tempStAm = await _StayAmenityService.GetAllStayAmenitysAsync();
            var tempAmenities = await _amenityService.GetAllAmenitiesAsync();//napravi service za stayamenity incahe nqma da izleznat
            foreach (var StA in tempStAm)
            {
                if (StA.StayId == tempStay.Id)
                {
                    foreach (var a in tempAmenities)
                    {
                        if (a.Id == StA.AmenityId)
                        {
                            model.Amenities.Add(a);
                        }
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> OfferPage(int id, OfferPageViewModel model)//copied from edit offer
        {
            return View(model);
        }
    }

}