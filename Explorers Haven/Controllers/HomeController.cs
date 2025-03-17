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
        public HomeController(IStayAmenityService StayAmenityService,IBookingService bookingService, IFavoriteService favoriteService, ITransportService transportService,UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IActivityService activyService, ITravelService travelService, ICommentService commentService, IAmenityService amenityService, IRatingService ratingService, IStayService stayService,IOfferService offerService, IUserService userService)
        {
            _StayAmenityService = StayAmenityService;
            _bookingService = bookingService;
            _transportService = transportService;
            _favoriteService=favoriteService;
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
                Transports=transports.ToList(),
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
            var fav = await _favoriteService.GetFavoriteAsync(x=>x.OfferId==tempOffer.Id&&x.UserId == userModel.Id);
            if (fav == null)
            {
                model.IsFavorited = false;
            }
            else
            { 
            model.IsFavorited= true;
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
            
            var tempRate = await _ratingService.GetAllRatingsAsync(x => x.OfferId == id);
            model.Ratings = tempRate.ToList();
            var tempCom = await _commentService.GetAllCommentsAsync(x => x.OfferId == id);
            model.Comments=tempCom.ToList();
            //Comments=tempCom.ToList(),
            if (tempRate.Count() != 0)
            {
                decimal rates = 0;
                foreach (var r in tempRate)
                {
                    rates += r.Stars;
                }
                decimal AverageRate;
                decimal ofst;
                if (tempOffer.Rating != null)
                {
                    rates =+ tempOffer.Rating.Value;
                    int countt = tempRate.Count() + 1;
                    AverageRate = rates/ countt;
                    ofst = Math.Round(AverageRate,2);
                }
                else 
                {
                    AverageRate = rates / tempRate.Count();
                    ofst = Math.Round(AverageRate,2);
                }
                model.OfferRatingStars = ofst;
                model.OfferRating = AverageRate;
            }
            else 
            {
                if (tempOffer.Rating != null)
                {
                    model.OfferRatingStars = Math.Round(tempOffer.Rating.Value);
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
                if (ac.OfferId==tempOffer.Id)
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
            var tempStAm =await  _StayAmenityService.GetAllStayAmenitysAsync();
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
            /*foreach (var a in Model.Amenities)
{ 
	<h1>@a.Name</h1>
}*/

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> OfferPage(int id, OfferPageViewModel model)//copied from edit offer
        {
            return View(model);
        }
    }
    /*
     @using Explorers_Haven.ViewModels.Main
@model OfferPageViewModel
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@Model.OfferName - Explorers Haven</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
        :root {
        --primary-color: #2681ff;
        --secondary-color: #ff6500;
        --text-color: #333;
        --light-gray: #f5f5f5;
        --medium-gray: #e0e0e0;
        --dark-gray: #666;
        --white: #fff;
        --border-radius: 8px;
        --box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }

        * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
        }

        body {
        background-color: #f5f7fa;
        color: var(--text-color);
        line-height: 1.6;
        }

        .container {
        max-width: 1200px;
        margin: 0 auto;
        padding: 20px;
        }

        /* Header section 
        .offer-header {
        display: flex;
        justify-content: space-between;
        align-items: flex-start;
        margin-bottom: 20px;
        padding-bottom: 20px;
        border-bottom: 1px solid var(--medium-gray);
}

        .offer - title {
flex: 1;
}

        .offer - title h1 {
        font-size: 28px;
margin - bottom: 10px;
color: #333;
        }

        .rating - container {
display: flex;
    align - items: center;
    margin - bottom: 10px;
}

        .stars {
        display: flex;
position: relative;
direction: rtl; /* Ensure the stars go from left to right 
        }

        .stars label
{
    font-size: 0;
    cursor: pointer; /* Make the stars clickable 
}

        .stars label:before {
        content: "★";
color: #ddd;
        font - size: 24px;
        }

        .stars input:checked ~label:before,
        .stars: not(:checked) > label:hover: before,
        .stars:not(:checked) > label:hover ~label:before {
color: #ffb700;
        }

        .stars input {
position: absolute;
right: -9999px;
}


        .rating - number {
    margin - left: 10px;
    font - size: 16px;
color: var(--dark - gray);
}

        .favorite - btn.favorite - checkbox {
display: none;
}

        .favorite - btn.favorite - btn - label {
    background - color: white;
display: flex;
    align - items: center;
gap: 14px;
padding: 10px 15px 10px 10px;
cursor: pointer;
    user - select: none;
    border - radius: 10px;
    box - shadow: rgba(149, 157, 165, 0.2) 0px 8px 24px;
color: black;
}

        .favorite - btn.favorite - checkbox:checked + .favorite - btn - label svg {
fill: hsl(0deg 100 % 50 %);
stroke: hsl(0deg 100 % 50 %);
animation: heartButton 1s;
}

keyframes heartButton {
    0 %

        {
    transform: scale(1);
    }

    25 % {
    transform: scale(1.3);
    }

    50 % {
    transform: scale(1);
    }

    75 % {
    transform: scale(1.3);
    }

    100 % {
    transform: scale(1);
    }

}

        .favorite - btn.favorite - checkbox + .favorite - btn - label.action {
position: relative;
overflow: hidden;
display: grid;
}

        .favorite - btn.favorite - checkbox + .favorite - btn - label.action span {
    grid - column - start: 1;
    grid - column - end: 1;
    grid - row - start: 1;
    grid - row - end: 1;
transition: all .5s;
}

        .favorite - btn.favorite - checkbox + .favorite - btn - label.action span.option - 1 {
transform: translate(0px, 0 %);
opacity: 1;
}

        .favorite - btn.favorite - checkbox:checked + .favorite - btn - label.action span.option - 1 {
transform: translate(0px, -100 %);
opacity: 0;
}

        .favorite - btn.favorite - checkbox + .favorite - btn - label.action span.option - 2 {
transform: translate(0px, 100 %);
opacity: 0;
}

        .favorite - btn.favorite - checkbox:checked + .favorite - btn - label.action span.option - 2 {
transform: translate(0px, 0 %);
opacity: 1;
}



        /* Main image 
        .main - image {
width: 100 %;
    margin - bottom: 20px;
    border - radius: var(--border - radius);
overflow: hidden;
}

        .main - image img {
width: 100 %;
height: 400px;
    object-fit: cover;
}

        /* Content sections 
        .content - section {
display: flex;
    margin - bottom: 30px;
    background - color: var(--white);
    border - radius: var(--border - radius);
    box - shadow: var(--box - shadow);
overflow: hidden;
}

        .description {
flex: 3;
padding: 20px;
}

        .description - title {
    font - size: 20px;
    margin - bottom: 10px;
color: var(--primary - color);
}

        .description - content {
    line - height: 1.8;
color: var(--text - color);
}

        .booking - details {
flex: 2;
padding: 20px;
    background - color: #f9f9f9;
        border - left: 1px solid var(--medium - gray);
}

        .price - container {
display: flex;
    justify - content: space - between;
    align - items: center;
    margin - bottom: 20px;
    padding - bottom: 15px;
    border - bottom: 1px solid var(--medium - gray);
}

        .price - label {
    font - size: 14px;
color: var(--dark - gray);
}

        .price - value {
    font - size: 24px;
    font - weight: bold;
color: var(--secondary - color);
}

form {
display: flex;
    flex - direction: column;
}

label {
    margin - bottom: 5px;
    font - size: 14px;
color: var(--dark - gray);
}

input[type = "number"],
        input[type = "date"] {
padding: 10px;
    margin - bottom: 15px;
border: 1px solid var(--medium - gray);
    border - radius: var(--border - radius);
}

button[type = "submit"],
        .book - now - btn {
padding: 12px 20px;
    margin - top: 10px;
    background - color: var(--secondary - color);
color: var(--white);
border: none;
    border - radius: var(--border - radius);
    font - size: 16px;
    font - weight: bold;
cursor: pointer;
transition: background - color 0.2s;
    text - align: center;
    text - decoration: none;
display: inline - block;
}

button[type = "submit"]:hover,
        .book - now - btn:hover {
    background - color: #e65c00;
        }

        .cancel - btn {
padding: 12px 20px;
    margin - top: 10px;
    background - color: var(--light - gray);
color: var(--dark - gray);
border: 1px solid var(--medium - gray);
    border - radius: var(--border - radius);
    font - size: 16px;
    font - weight: bold;
cursor: pointer;
transition: background - color 0.2s;
    text - align: center;
    text - decoration: none;
display: inline - block;
    margin - left: 10px;
}

        .cancel - btn:hover {
    background - color: #e0e0e0;
        }

        /* Accommodation section 
        .accommodation - section {
    margin - bottom: 30px;
}

        .section - title {
    font - size: 24px;
    margin - bottom: 15px;
color: var(--primary - color);
}

        .accommodation - card {
    background - color: var(--white);
    border - radius: var(--border - radius);
    box - shadow: var(--box - shadow);
overflow: hidden;
display: flex;
}

        .accommodation - image {
flex: 1;
    max - width: 300px;
}

        .accommodation - image img {
width: 100 %;
height: 100 %;
    object-fit: cover;
}

        .accommodation - details {
flex: 2;
padding: 20px;
}

        .accommodation - title {
display: flex;
    justify - content: space - between;
    align - items: center;
    margin - bottom: 10px;
}

        .accommodation - name {
    font - size: 20px;
color: var(--text - color);
}

        .stay - stars {
color: #ffb700;
        font - size: 16px;
}

        .stay - price {
    font - size: 20px;
    font - weight: bold;
color: var(--secondary - color);
    margin - top: 10px;
}

        /* Travel details section 
        .travel - section {
    margin - bottom: 30px;
}

        .travel - card {
    background - color: var(--white);
    border - radius: var(--border - radius);
    box - shadow: var(--box - shadow);
padding: 20px;
    margin - bottom: 15px;
display: flex;
    align - items: center;
}

        .travel - point {
flex: 1;
    text - align: center;
}

        .travel - location {
    font - size: 18px;
    font - weight: bold;
color: var(--text - color);
}

        .travel - date {
    font - size: 14px;
color: var(--dark - gray);
}

        .travel - icon {
flex: 0.5;
    text - align: center;
}

        .travel - icon img {
width: 30px;
opacity: 0.6;
}

        .travel - transport {
flex: 1;
    text - align: center;
padding: 8px 15px;
    background - color: var(--light - gray);
    border - radius: 20px;
    font - size: 14px;
color: var(--dark - gray);
}

        /* Activities section 
        .activities - section {
    margin - bottom: 30px;
}

        .activities - grid {
display: grid;
    grid - template - columns: repeat(auto - fill, minmax(280px, 1fr));
gap: 20px;
}

        .activity - card {
    background - color: var(--white);
    border - radius: var(--border - radius);
    box - shadow: var(--box - shadow);
overflow: hidden;
}

        .activity - image {
height: 180px;
overflow: hidden;
}

        .activity - image img {
width: 100 %;
height: 100 %;
    object-fit: cover;
}

        .activity - name {
padding: 15px;
    font - size: 16px;
    font - weight: bold;
color: var(--text - color);
}

        /* Reviews section *
        .reviews - section {
    margin - bottom: 30px;
}

        .review - form {
    background - color: var(--white);
    border - radius: var(--border - radius);
    box - shadow: var(--box - shadow);
padding: 20px;
    margin - bottom: 20px;
}

        .review - form input[type = "text"] {
width: 100 %;
padding: 12px;
border: 1px solid var(--medium - gray);
    border - radius: var(--border - radius);
    margin - bottom: 10px;
}

        .review - form button {
padding: 10px 20px;
    background - color: var(--primary - color);
color: var(--white);
border: none;
    border - radius: var(--border - radius);
cursor: pointer;
transition: background - color 0.2s;
}

        .review - form button: hover {
    background - color: #1a6ac2;
        }

        .comment - list {
    background - color: var(--white);
    border - radius: var(--border - radius);
    box - shadow: var(--box - shadow);
padding: 20px;
}

        .comment - item {
display: flex;
    margin - bottom: 20px;
    padding - bottom: 20px;
    border - bottom: 1px solid var(--medium - gray);
}

        .comment - item:last - child {
    margin - bottom: 0;
    padding - bottom: 0;
    border - bottom: none;
}

        .comment - avatar {
width: 50px;
height: 50px;
    border - radius: 50 %;
overflow: hidden;
    margin - right: 15px;
}

        .comment - avatar img {
width: 100 %;
height: 100 %;
    object-fit: cover;
}

        .comment - content {
flex: 1;
    line - height: 1.5;
}

        .booking - actions {
display: flex;
    justify - content: space - between;
    margin - top: 20px;
}
    </ style >
</ head >
< body >
    < div class= "container" >
        < div class= "offer-header" >
            < div class= "offer-title" >
                < h1 > @Model.OfferName </ h1 >
                < div class= "rating-container" >
                    @for(int i = 0; i < Model.OfferRatingStars; i++)
                    {
                        < div class= "stay-stars" >
                            < span >★</ span >
                        </ div >
                    }

                    < span class= "rating-number" > @Model.OfferRating </ span >
                </ div >
            </ div >
            < form method = "post" action = "/Favorite/Favorite" >
                < div class= "favorite-btn" >
                    < input type = "hidden" name = "id" value = "@Model.OfferId" />
                    < input type = "checkbox" id = "favorite" class= "favorite-checkbox" name = "favorite-checkbox" value = "true" @(Model.IsFavorited.Value ? "checked" : "") >
                    < label class= "favorite-btn-label" for= "favorite" >
                        < svg xmlns = "http://www.w3.org/2000/svg" width = "24" height = "24" viewBox = "0 0 24 24" fill = "none" stroke = "currentColor" stroke - width = "2" stroke - linecap = "round" stroke - linejoin = "round" class= "feather feather-heart" >
                            < path d = "M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z" ></ path >
                        </ svg >
                        < span > Add to Favorites</span>
                    </label>
                </div>
                <button type = "submit" style= "display:none;" > Submit </ button >
            </ form >
        </ div >

        < div class= "main-image" >
            < img src = "@Model.OfferPic" alt = "@Model.OfferName" >
        </ div >

        < div class= "content-section" >
            < div class= "description" >
                < div class= "description-title" > Description </ div >
                < div class= "description-content" > @Model.OfferDisc </ div >
                < div class= "description-content" > Bookings available every week the same day as starting date. Last available week for booking is on the last date.</div>
                <div class= "description-content" > Starting Date: @Model.OfferStart </ div >
                < div class= "description-content" > Last Date: @Model.OfferLast </ div >
                < div class= "description-content" > Max number of people: @Model.OfferPeople </ div >
                < div class= "description-content" > Duration in days: @Model.OfferDays </ div >
            </ div >

            < div class= "booking-details" >
                < div class= "price-container" >
                    < div class= "price-label" > Price per person</div>
                    <div class= "price-value" > @Model.OfferPrice лв </ div >
                </ div >
                < form method = "post" action = "/Booking/Book" >
                    < !--Hidden field for offer ID -->
                    <input type="hidden" name="id" value="@Model.OfferId" />

                    <!-- People Count Input -->
                    <label for="ppl">People Count:</ label >
                    < input type = "number" id = "ppl" name = "ppl" required min = "1" />

                    < !--Discounted People Count Input -->
                    @if (Model.IsOnDiscount == true)
                    {
                        <label for="discppl">Elders or Kids Count with @Model.OfferDiscount% discount:</ label >
                        < input type = "number" id = "discppl" name = "discppl" required min = "0" />
                    }
                    else
{
                        < input type = "hidden" id = "discppl" name = "discppl" value = "1" />
                    }


                    < !--Start Date Input -->
                    <label for="st">Start Date:</ label >
                    < input type = "date" id = "st" name = "st" required />

                    < !--Submit Button-- >
                    < button type = "submit" > Book Now </ button >
                </ form >
            </ div >
        </ div >

        < div class= "accommodation-section" >
            < h2 class= "section-title" > Accommodation Details </ h2 >
            < div class= "accommodation-card" >
                < div class= "accommodation-image" >
                    < img src = "@Model.StayPic" alt = "@Model.StayName" >
                </ div >
                < div class= "accommodation-details" >
                    < div class= "accommodation-title" >
                        < h3 class= "accommodation-name" > @Model.StayName </ h3 >
                        < div class= "stay-stars" >
                            @for(int i = 0; i < Model.StayStars.Value; i++)
                            {
                                < span >★</ span >
                            }
                        </ div >
                    </ div >
                    < p > @Model.StayDisc </ p >
                    < div class= "stay-price" > @Model.StayPrice лв </ div >
                </ div >
            </ div >
        </ div >
        < div class= "activities-section" >
            < h2 class= "section-title" > Amenitites </ h2 >
            < div class= "activities-grid" >
                @foreach(var a in Model.Amenities)
                {
                    < div class= "activity-name" > @a.Icon </ div >
                    < div class= "activity-name" > @a.Name </ div >
                }
            </ div >
        </ div >
        < div class= "travel-section" >
            < h2 class= "section-title" > Travel Details </ h2 >
            @foreach(var a in Model.Travels)
            {
                < div class= "travel-card" >
                    < div class= "travel-point" >
                        < div class= "travel-location" > @a.Start </ div >
                    </ div >
                    @foreach(var tr in Model.Transports)
                    {
    if (@a.TransportId == tr.Id && tr.Name == "Plane")
    {
                            < div class= "travel-transport" > @tr.Name </ div >
                    < div class= "travel-icon" >


                        < img src = "/Images/Plane.svg" alt = "Plane" />
                    </ div >
                    }
                    if (@a.TransportId == tr.Id && tr.Name == "Train")
{
                    < div class= "travel-icon" >
                                < div class= "travel-transport" > @tr.Name </ div >
                                < img src = "/Images/Train.svg" alt = "Train" />
                    </ div >
                    }
                        if (@a.TransportId == tr.Id && tr.Name == "Boat")
{
                            < div class= "travel-icon" >
                                < div class= "travel-transport" > @tr.Name </ div >
                                < img src = "/Images/boat.svg" alt = "boat" />
                            </ div >
                        }
                        if (@a.TransportId == tr.Id && tr.Name == "Custom")
{
                            < div class= "travel-icon" >
                                < div class= "travel-transport" > @tr.Name </ div >
                                < img src = "/Images/custom.svg" alt = "custom" />
                            </ div >
                        }
                    }



                    < div class= "travel-point" >
                        < div class= "travel-location" > @a.Finish </ div >
                    </ div >


                </ div >
            }
        </ div >

        < div class= "activities-section" >
            < h2 class= "section-title" > Activities </ h2 >
            < div class= "activities-grid" >
                @foreach(var a in Model.Activities)
                {
                    < div class= "activity-card" >
                        < div class= "activity-image" >
                            < img src = "@a.CoverImage" alt = "@a.Name" >
                        </ div >
                        < div class= "activity-name" > @a.Name </ div >
                    </ div >
                }
            </ div >
        </ div >

        < div class= "reviews-section" >
            < h2 class= "section-title" > Reviews </ h2 >
            < div class= "review-form" >
                < form method = "post" action = "/Rating/Rate" id = "ratingForm" >
                    < input type = "hidden" name = "id" value = "@Model.OfferId" />
                    < div class= "stars" >
                        @if(Model.OfferRatingStars == 1)
                        {
                            < input value = "1" name = "rating" id = "star1" type = "radio" checked>
                        }
                        else
    {
                            < input value = "1" name = "rating" id = "star1" type = "radio" >
                        }
                        < label for= "star1" ></ label >
                        @if(Model.OfferRatingStars == 2)
                        {
                            < input value = "2" name = "rating" id = "star2" type = "radio" checked>
                        }
                        else
        {
                            < input value = "2" name = "rating" id = "star2" type = "radio" >
                        }
                        < label for= "star2" ></ label >
                        @if(Model.OfferRatingStars == 3)
                        {
                            < input value = "3" name = "rating" id = "star3" type = "radio" checked>
                        }
                        else
            {
                            < input value = "3" name = "rating" id = "star3" type = "radio" >
                        }
                        < label for= "star3" ></ label >
                        @if(Model.OfferRatingStars == 4)
                        {
                            < input value = "4" name = "rating" id = "star4" type = "radio" checked>
                        }
                        else
                {
                            < input value = "4" name = "rating" id = "star4" type = "radio" >
                        }
                        < label for= "star4" ></ label >
                        @if(Model.OfferRatingStars == 5)
                        {
                            < input value = "5" name = "rating" id = "star5" type = "radio" checked>
                        }
                        else
                    {
                            < input value = "5" name = "rating" id = "star5" type = "radio" >
                        }
                        < label for= "star5" ></ label >
                    </ div >
                    < button type = "submit" style = "display:none;" > Submit </ button >
                </ form >
                < form method = "post" action = "/Comment/WriteComment" >
                    < input type = "hidden" name = "id" value = "@Model.OfferId" />
                    < input type = "text" id = "comment" name = "comment" placeholder = "Share your experience..." />
                    < button type = "submit" > Submit </ button >
                </ form >
            </ div >

            < div class= "comment-list" id = "commentsSection" >
                @foreach(var a in Model.Comments)
                {
                    < div class= "comment-item" >
                        @foreach(var u in Model.Users)
                        {
    if (u.Id == a.UserId)
    {
                                < div class= "comment-avatar" >
                                    < img src = "@u.ProfilePicture" alt = "User profile" >
                                </ div >
                            }
                        }
                        @foreach(var u in Model.Ratings)
                        {
    if (u.UserId == a.UserId)
    {
        @for(int i = 0; i < u.Stars; i++)
                                {
                                    < div class= "stay-stars" >
                                    < span >★</ span >
                                    </ div >
                                }
                            }
                        }
                        < div class= "comment-content" > @a.Content </ div >
                    </ div >
                }
            </ div >
        </ div >
    </ div >
    < script >
        document.getElementById('favorite').addEventListener('change', function() {
    this.form.submit();  // Submit the form when checkbox is toggled
});


const form = document.getElementById('ratingForm');

// Add event listener to the form
form.addEventListener('change', function(event) {
    // Check if a radio button is selected
    if (event.target.name === 'rating') {
        // Automatically submit the form when a radio button is selected
        form.submit();
    }
});
    </ script >
</ body >
</ html > */
}
