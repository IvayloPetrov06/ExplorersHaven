using System.Collections.Generic;
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
        IUserService userService;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public HomeController(UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IActivityService activyService, ITravelService travelService, ICommentService commentService, IAmenityService amenityService, IRatingService ratingService, IStayService stayService,IOfferService offerService, IUserService userService)
        {
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
            //var trav = _offerService.GetOfferByIdAsync(Id);
            //if (trav == null) { return NotFound(); }
            //return View(trav);
            //Offer offers = await _offerService.GetOfferByIdAsync(Id);
            //if (offers == null) { return NotFound(); }
            // View(offers);
            var tempOffer = await _offerService.GetOfferByIdAsync(id);
            var tempStay = await _stayService.GetStayByIdAsync(tempOffer.StayId.Value);
            

            var model = _offerService.GetAll().Where(x => x.Id == id).Include(x => x.User)
            .Select(x => new OfferPageViewModel()
            {
                OfferId = tempOffer.Id,
                OfferPic = x.BackImage,
                OfferPrice = x.Price,
                OfferName = x.Name,
                OfferDisc = x.Disc,
                StayName = tempStay.Name,
                StayPic = tempStay.Image,
                StayPrice = tempStay.Price,
                StayDisc = tempStay.Disc,
                UserId = x.UserId
            }).FirstOrDefault();
            var tempRate = await _ratingService.GetAllRatingsAsync(x => x.OfferId == id);
            var tempCom = await _commentService.GetAllCommentsAsync(x => x.OfferId == id);

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
            var tempAmenities = _amenityService.GetAll();//napravi service za stayamenity incahe nqma da izleznat
            foreach (var a in tempAmenities)
            {
                foreach (var sa in a.StayAmenities)
                {
                    if (sa.StayId == tempStay.Id)
                    {
                        model.Amenities.Add(a);
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
     * @using Explorers_Haven.ViewModels.Main
@model OfferPageViewModel

<div class="offer-container">
    <div class="offer-header">
        <h1 class="offer-name">@Model.OfferName</h1>
        <img class="offer-image" src="@Model.OfferPic" alt="@Model.OfferName">
        <p class="text-custom">@Model.OfferDisc</p>
    </div>

    <div class="stay-section">
        <div class="stay-image-container">
            <img class="stay-image" src="@Model.StayPic" alt="@Model.StayName">
        </div>
        <div class="stay-details">
            <h2 class="stay-name">@Model.StayName</h2>
            <div class="stay-stars">@Model.StayStars.ToString()</div>
            <div class="stay-price">@Model.StayPrice лв</div>
            <div class="text-custom">@Model.StayDisc</div>
        </div>
    </div>

    @if (Model.Activities.Any())
    {
        <div class="activities-section">
            <h3 class="section-title">Activities</h3>
            <div class="activities-list">
                @foreach (var a in Model.Activities)
                {
                    <div class="activity-item">
                        <h4 class="activity-name">@a.Name</h4>
                    </div>
                }
            </div>
        </div>
    }

    @if (Model.Travels.Any())
    {
        <div class="travel-section">
            <h3 class="section-title">Travel Details</h3>
            @foreach (var a in Model.Travels)
            {
                <div class="travel-item">
                    <div class="travel-row">
                        <div class="travel-col">
                            <div class="travel-label">Departure</div>
                            <div class="travel-value">@a.Start</div>
                            <div class="travel-value">@a.DateStart.ToString()</div>
                        </div>
                        <div class="travel-col">
                            <div class="travel-label">Arrival</div>
                            <div class="travel-value">@a.Finish</div>
                            <div class="travel-value">@a.DateFinish</div>
                        </div>
                    </div>

                    <div class="travel-col">
                        <div class="travel-label">Transport</div>

                        <span class="transport-icon">
                            @*takeoff-the-plane-svgrepo-com <img src="plane.jpg" alt="External SVG Image" />
                            <object data="C:\Users\user\source\repos\Ivo2\ExplorersHaven\Explorers Haven\wwwroot\Im/takeoff-the-plane-svgrepo-com.svg" type="image/svg+xml">
                                <img src="C:\Users\user\source\repos\Ivo2\ExplorersHaven\Explorers Haven\wwwroot\Im/plane.jpg" />
                            </object>*@
                            <img class="mini-icon" src="~/Images/Plane.svg" alt="Plane" />
                        </span> @a.Transport
                    </div>
                </div>
            }
        </div>
    }

    <div class="booking-section">
        <div class="total-price">@Model.OfferPrice лв</div>
        <div class="booking-actions">
            <a class="btn btn-book" asp-controller="Booking" asp-action="Book" asp-route-id="@Model.OfferId">Book Now</a>
            <a class="btn btn-cancel" asp-controller="Booking" asp-action="Cancel" asp-route-id="@Model.OfferId">Cancel</a>
        </div>
    </div>
</div>
<style>
    /* Base styles and reset 
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
    }

body {
        font-family: 'Segoe UI', Arial, sans-serif;
background - color: #f5f5f5;
        color: #333;
        line - height: 1.6;
    }

    /* Main container 
    .offer - container {
    max - width: 1200px;
margin: 20px auto;
    background - color: #fff;
        border - radius: 8px;
    box - shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
padding: 30px;
}

path[Attributes Style] {
d: path("M 21.0001 20 C 21.5524 20 22.0001 20.4477 22.0001 21 C 22.0001 21.5128 21.6141 21.9355 21.1167 21.9933 L 21.0001 22 L 3.00008 22 C 2.44779 22 2.00008 21.5523 2.00008 21 C 2.00008 20.4872 2.38612 20.0645 2.88346 20.0067 L 3.00008 20 L 21.0001 20 Z M 7.93041 4.17678 L 14.493 9.53711 L 14.493 9.53711 L 19.0655 8.68222 C 20.1891 8.47192 21.3346 8.91842 22.0194 9.83365 L 22.1806 10.0527 C 22.3605 10.3096 22.4966 10.577 22.4262 10.9212 C 22.205 12.002 21.3922 12.8651 20.3266 13.1506 L 5.20745 17.2017 C 4.59963 17.3646 3.95476 17.1308 3.59258 16.6162 L 0.929819 12.8329 C 0.72978 12.5486 0.873576 12.1521 1.20929 12.0621 L 2.83331 11.627 C 3.31775 11.4972 3.83501 11.6181 4.21174 11.9491 L 4.95832 12.6052 L 4.96856 12.607 L 4.96856 12.607 L 9.16743 10.9666 C 9.17376 10.9641 9.17583 10.9562 9.17152 10.9509 L 4.44679 5.19599 C 4.21618 4.9151 4.35278 4.48982 4.70382 4.39576 L 6.59315 3.88951 C 7.05916 3.76464 7.55679 3.87154 7.93041 4.17678 Z");
fill: rgb(9, 36, 75);
}

    /* Offer header section 
    .offer - header {
    border - bottom: 1px solid #eaeaea;
        padding - bottom: 20px;
    margin - bottom: 30px;
}

    .offer - name {
    font - size: 28px;
    font - weight: 700;
color: #333;
        margin - bottom: 10px;
}

    .offer - desc {
    font - size: 16px;
color: #666;
        margin - bottom: 20px;
    line - height: 1.5;
}

    /* Stay info section 
    .stay - section {
display: flex;
    flex - wrap: wrap;
gap: 30px;
    margin - bottom: 30px;
}

    .stay - image - container {
flex: 1;
    min - width: 300px;
}

    .stay - image {
width: 100 %;
height: 350px;
    object-fit: cover;
    border - radius: 8px;
}

    .offer - image {
width: 100 %;
height: auto;
    max - height: 500px;
    object-fit: cover;
    border - radius: 8px;
    margin - bottom: 25px;
}

    .stay - details {
flex: 1;
    min - width: 300px;
}
    .text - custom {
    font - size: 20px;
    font - weight: 600;
}

    .stay - name {
    font - size: 24px;
    font - weight: 600;
color: #0071c2;
        margin - bottom: 10px;
}

    .stay - stars {
color: #ffc107;
        font - size: 20px;
    margin - bottom: 15px;
}

    .stay - price {
    font - size: 26px;
    font - weight: bold;
color: forestgreen;
    margin - bottom: 15px;
}

    /* Activities section 
    .activities - section {
    margin - bottom: 30px;
}

    .section - title {
    font - size: 22px;
    font - weight: 600;
color: #333;
        margin - bottom: 15px;
    padding - bottom: 10px;
    border - bottom: 1px solid #eaeaea;
    }

    .activities - list {
display: flex;
    flex - wrap: wrap;
gap: 15px;
}

    .activity - item {
    background - color: #f9f9f9;
        padding: 15px;
    border - radius: 6px;
width: calc(33.33 % -10px);
    min - width: 200px;
}

    .activity - name {
    font - size: 18px;
    font - weight: 500;
color: #333;
    }

    /* Travel details section 
    .travel - section {
    margin - bottom: 30px;
}

    .travel - item {
    background - color: #f9f9f9;
        border - radius: 8px;
padding: 20px;
    margin - bottom: 20px;
}

    .travel - row {
display: flex;
    margin - bottom: 15px;
    align - items: center;
}

    .travel - col {
flex: 1;
}

    .travel - label {
    font - size: 14px;
color: #666;
        margin - bottom: 5px;
}

    .travel - value {
    font - size: 18px;
    font - weight: 500;
color: #333;
    }

    .mini - icon {
width: 18px;
height: 18px;
}

    .transport - info {
padding: 15px 0;
    font - size: 18px;
    font - weight: 500;
color: #0071c2;
        flex: 1;
display: flex;
    align - items: center;
}

    .transport - icon {
    margin - right: 10px;
    font - size: 20px;
}

    /* Price and booking section 
    .booking - section {
    background - color: #f9f9f9;
        border - radius: 8px;
padding: 25px;
display: flex;
    flex - wrap: wrap;
    justify - content: space - between;
    align - items: center;
gap: 20px;
}

    .total - price {
    font - size: 28px;
    font - weight: bold;
color: forestgreen;
}

    .booking - actions {
display: flex;
gap: 15px;
}

    .btn {
        display: inline - block;
padding: 12px 24px;
border - radius: 4px;
text - decoration: none;
font - weight: 600;
font - size: 16px;
text - align: center;
transition: all 0.3s ease;
    }

    .btn - book {
    background - color: #0071c2;
        color: white;
    min - width: 150px;
    box - shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}

        .btn - book:hover {
            background-color: #005999;
        }

    .btn - cancel {
    background - color: #fff;
        color: #0071c2;
        border: 1px solid #0071c2;
        box - shadow: rgba(0, 0, 0, 0.35) 0px 5px 15px;
}

        .btn - cancel:hover {
            background-color: #f5f9fc;
        }

    /* Utility class for travel details grid *
    .el1 {
        flex: 1;
min - width: 200px;
    }

    /* Responsive adjustments 
    media(max - width: 768px) {
        .stay - section, .travel - row, .booking - section

    {
        flex - direction: column;
    }

    .activity - item {
    width: 100 %;
    }

    .booking - actions {
    width: 100 %;
    }

    .btn {
    width: 100 %;
    }

}

/* Convert regular h1 headings to appropriate styles based on context 
h1 {
        font-size: 1.2rem;
margin - bottom: 10px;
    }
</ style > @using Explorers_Haven.ViewModels.Main
@model OfferPageViewModel
<h1>@Model.OfferName</h1>
<img src="@Model.OfferPic" alt="@Model.OfferName">
<h1>@Model.OfferDisc</h1>
<h1>@Model.StayName</h1>
<h1>@Model.StayStars</h1>
<h1>@Model.StayPrice</h1>
<img src="@Model.StayPic" alt="@Model.StayName">

@foreach (var a in Model.Activities)
{
	<h1>@a.Name</h1>
}
@foreach (var a in Model.Travels)
{
	<div class="el1">
		<h1>@a.Start</h1>
		<h1>@a.DateStart.ToString()</h1>
	</div>
	<div class="el1">
		<h1>@a.Finish</h1>
		<h1>@a.DateFinish</h1>
	</div>
	<h1>@a.Transport</h1>
}
<h1>@Model.OfferPrice</h1>
<a asp-controller="Booking" asp-action="Book">Book</a>
<a asp-controller="Booking" asp-action="Cancel">Cancel</a>*/
}
