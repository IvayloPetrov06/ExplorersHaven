﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.DataAccess;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Activity;
using Explorers_Haven.ViewModels.Offer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class OfferController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IOfferService _offerService;
        private readonly ITravelService _travelService;
        private readonly IActivityService _activityService;
        private readonly IFavoriteService _favoriteService;
        private readonly IBookingService _bookingService;
        private readonly IStayService _stayService;
        private readonly ICommentService _commentService;
        IUserService userService;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public OfferController(IActivityService activityService, IBookingService bookingService,IFavoriteService favoriteService, ITravelService travelService, ICommentService commentService,UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IOfferService offerService, IStayService stayService, IUserService userService)
        {
            _activityService = activityService;
            _bookingService = bookingService;
            _favoriteService = favoriteService;
            _travelService = travelService;
            this.userService = userService;
            _offerService = offerService;
            _stayService = stayService;
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

        public async Task<IActionResult> Index(OfferFilterViewModel? filter)
        {
            var query = _offerService.GetAll().AsQueryable();
            var filterModel = new OfferFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {
                /*<th style="width: 50px">#</th>
                                <th>Name</th>
                                <th>Price</th>
                                <th>Rating</th>
                                <th>Comments</th>
                                <th>Actions</th>*/

                var model = _offerService.CombinedInclude().Include(x => x.User).Select(x => new OfferViewModel()
                {
                    OfferId = x.Id,
                    OfferName= x.Name,
                    OfferPic = x.CoverImage,
                    UserName = x.User.Username,
                    OfferPrice = x.Price,
                    Comments = x.Comments.ToList(),
                    //OfferRatingStars = x.R
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
                    OfferId = x.Id,
                    OfferName = x.Name,
                    OfferPic = x.CoverImage,
                    UserName = x.User.Username,
                    OfferPrice = x.Price,
                    Comments = x.Comments.ToList(),

                }).ToList(),
                    Search = filter.Search
                };
            };

            return View(filterModel);
        }


        public async Task<IActionResult> AllOffer(OfferFilterViewModel? filter)
        {
            var query = _offerService.GetAll().AsQueryable();
            //var playlists = await playlistService.GetAllPlaylistsAsync();


            if (string.IsNullOrEmpty(filter.Search))
            {
                var model = _offerService.CombinedInclude().Include(x => x.User).ThenInclude(x => x.Offers).Select(x => new OfferViewModel()
                {
                    OfferId = x.Id,
                    OfferName = x.Name,
                    OfferPic = x.CoverImage,
                    UserName = x.User.Username,
                    OfferPrice = x.Price,
                    Comments = x.Comments.ToList(),

                }).ToList();

                //

                foreach (var o in model)
                {
                    var tempOffer = await _offerService.GetOfferByIdAsync(o.OfferId);
                    var tempCom = await _commentService.GetAllCommentsAsync(x => x.OfferId == o.OfferId);
                    o.Comments = tempCom.ToList();
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
                            rates += tempOffer.Rating.Value;
                            int countt = tempCom.Count() + 1;
                            AverageRate = rates / countt;
                            ofst = Math.Round(AverageRate, 2);
                        }
                        else//ako nqma
                        {
                            AverageRate = rates / tempCom.Count();
                            ofst = Math.Round(AverageRate, 2);
                        }
                        o.OfferRating = AverageRate;
                        o.OfferRatingStars = ofst;
                    }
                    else//ako nqma reviewta
                    {
                        if (tempOffer.Rating != null)
                        {
                            o.OfferRatingStars = Math.Round(tempOffer.Rating.Value, 2);
                            o.OfferRating = tempOffer.Rating;
                        }
                        else
                        {
                            o.OfferRatingStars = 3;
                            o.OfferRating = 3;
                        }

                    }
                }
                //


                var filterModel = new OfferFilterViewModel
                {
                    Offers = model,

                };
                var sortedList = filterModel.Offers.OrderBy(x => x.OfferName).ToList();
                filterModel.Offers = sortedList;
                return View(filterModel);
            }
            else
            {

                if (!string.IsNullOrEmpty(filter.Search))
                {
                    query = query.Where(x => x.Name == filter.Search);
                }


                var filterModel = new OfferFilterViewModel
                {
                    Offers = query.Include(x => x.User).ThenInclude(x => x.Offers).Select(x => new OfferViewModel()
                    {
                        OfferId = x.Id,
                        OfferName = x.Name,
                        UserName = x.User.Username,
                        OfferPic = x.CoverImage
                    }).ToList(),
                    Search = filter.Search
                };
                return View(filterModel);
            }

        }
        public async Task<IActionResult> ListOffers()
        {
            IEnumerable<Offer> offers = await _offerService.GetAllOfferAsync();
            return View(offers);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                
                await _offerService.DeleteOfferByIdAsync(id);
                await _commentService.DeleteAllCommentsByOffers(id);
                await _activityService.DeleteAllActivitysByOffers(id);
                await _travelService.DeleteAllTravelsByOffers(id);
                await _favoriteService.DeleteAllFavoritesByOffers(id);
                await _bookingService.DeleteAllBookingsByOffers(id);
            }
            return RedirectToAction("AllOffer");
        }
        public async Task<IActionResult> EditOffer(int id)
        {
            //comments travel activity booking favorite
            

            var model = _offerService.GetAll().Where(x => x.Id == id).Include(x => x.User)
                .Select(x => new EditOfferViewModel()
                {
                    OfferCover = x.CoverImage,
                    Price = x.Price,
                    Name = x.Name,
                    UserId = x.UserId,
                    UserList = new SelectList(userService.GetAll(), "Id", "Username"),
                    StayId = x.StayId
                }).FirstOrDefault();

            var stays = _stayService.GetAll();
            ViewBag.Stays = new SelectList(stays, "Id", "Name");

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditOffer(int id, EditOfferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.UserList = new SelectList(await userService.GetAllUsersAsync(), "Id", "Username");

                return View(model);
            }

            if (model.ImageFile != null)
            {
                var imageUploadResult = await cloudService.UploadImageAsync(model.ImageFile);

                Offer offer = _offerService.GetAll().Where(x => x.Id == id).FirstOrDefault();
                offer.Name = model.Name;
                offer.Price = model.Price;
                offer.CoverImage = imageUploadResult;
                offer.UserId = model.UserId;
                offer.StayId = model.StayId;

                await _offerService.UpdateOfferAsync(offer);
                return RedirectToAction("Index");
            }
            else
            {
                Offer offer = _offerService.GetAll().Where(x => x.Id == id).FirstOrDefault();
                offer.Name = model.Name;
                offer.Price = model.Price;
                offer.CoverImage = model.OfferCover;
                offer.UserId = model.UserId;
                offer.StayId = model.StayId;

                await _offerService.UpdateOfferAsync(offer);
                return RedirectToAction("Index");
            }
            /*if (ModelState.IsValid)
            {
                //_offerService.UpdateOfferAsync(obj);
                await _offerService.UpdateOfferAsync(obj);
                TempData["success"] = "Успешно редактиран запис";
                return RedirectToAction("ListOffers");
            }
            else
            {
                TempData["error"] = "Неуспешна редакция";
                return View(obj);
            }*/
        }
        public async Task<IActionResult> AddOffer()
        {
            var stays = _stayService.GetAll();
            ViewBag.Stays = new SelectList(stays, "Id", "Name");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddOffer(AddOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                // var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                //User user = await userService.GetUserAsync(x => x.UserIdentity.Email == tempUser.Email);

                //var imageUploadResult = await cloudService.UploadImageAsync(model.Picture);

                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);

                // Check if tempUser is null
                if (tempUser == null)
                {
                    // Handle the case when the user is not found
                    TempData["error"] = "User not found.";
                    return RedirectToAction("Login", "Account"); // Or wherever you want to redirect the user
                }

                // Log tempUser for debugging
                Console.WriteLine($"Found user: {tempUser.Email}");

                // Fetch the user from the user service
                //User user = await userService.GetUserAsync(x => x.UserIdentity.Email == tempUser.Email);

                //var normalizedEmail = tempUser.Email.Trim().ToLower();
                //User user = await userService.GetUserAsync(x => x.UserIdentity.Email.Trim().ToLower() == normalizedEmail);
                var tempUserEmail = tempUser.Email;
                TempData["error"] = $"TempUser email: {tempUserEmail}";

                var user = await userService.GetUserAsync(x => x.Email == tempUserEmail);
                TempData["error"] = $"User from userService: {user?.Email}";

                if (user == null)
                {
                    // Handle the case when userService doesn't find the user
                    TempData["error"] = "User details not found in the database.";
                    return RedirectToAction("Login", "Account"); // Or handle accordingly
                }

                // Log the user for debugging
                Console.WriteLine($"Found user in userService: {user.Email}");

                // Proceed with image upload
                var imageUploadResult1 = await cloudService.UploadImageAsync(model.Picture);

                var imageUploadResult2 = await cloudService.UploadImageAsync(model.BackPicture);





                Offer offer = new Offer
                {
                    Name = model.Name,
                    Price = model.Price,
                    UserId = user.Id,
                    CoverImage = imageUploadResult1,
                    StayId = model.StayId
                };
                offer.UserId = user.Id;
                await _offerService.AddOfferAsync(offer);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            /*if (ModelState.IsValid)
            {

                await _offerService.AddOfferAsync(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListOffers");
            }
            return View();*/
        }

        
    }

}

/* html index old:
         * <div class="text-center">
    <h1 class="display-4">Оферти</h1>
    <p>Избирайте от хиляди оферти!</p>
</div>

<h2>Търсете оферти</h2>
<form asp-action="Index" method="get" class="mb-4">

    <!--<div class="form-group">
        <label for="Id">Id</label>
        <input type="text" asp-for="Id" class="form-control" />
        <select asp-for="Id" asp-items="Model.Offers" class="form-control">
            <option value="">All Ids</option>
        </select>
    </div>-->

    <div class="form-group">
        <label for="Name">Име</label>
        <input type="text" asp-for="Search" class="form-control" />
    </div>

    <button type="submit" class="btn btn-primary">Покажи</button>

</form>


<h2>Всички налични оферти</h2>
<table class="table tabl-striped">
    <thead>
        <tr>
            <th>ИД</th>
            <th>Име</th>
            <th>Цена</th>
        </tr>
    </thead>
    <tbody>
        @if (Model.Offers.Count > 0)
        {
            @foreach (var offer in Model.Offers)
            {
                <tr>
                    <td>@offer.Id</td>
                    <td>@offer.Name</td>
                    <td>@offer.Price</td>
                </tr>
            }
        }
    </tbody>

</table>
         * 
         * html shared
         *
         * <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Offer" asp-action="Index">Search</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Offer" asp-action="ListOffers">ListOffers</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Trip" asp-action="Index">Search</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Trip" asp-action="ListTrips">ListTrips</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Stay" asp-action="Index">Search</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Stay" asp-action="ListStays">ListStays</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Activity" asp-action="Index">Search</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Activity" asp-action="ListActivities">ListActivities</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Travel" asp-action="Index">Search</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Travel" asp-action="ListTravels">ListTravels</a>
                        </li>
         */
