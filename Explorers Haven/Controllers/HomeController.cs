using CloudinaryDotNet;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Main;
using Explorers_Haven.ViewModels.Offer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        IUserService userService;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public HomeController(UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IActivityService activyService, ITravelService travelService, IAmenityService amenityService, IStayService stayService,IOfferService offerService, IUserService userService)
        {
            _offerService = offerService;
            this.userService = userService;
            _activityService = activyService;
            _travelService = travelService;
            _amenityService = amenityService;
            _stayService = stayService;

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
    /*@using Explorers_Haven.ViewModels.Main
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
