using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.DataAccess;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Offer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class OfferController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IOfferService _offerService;
        private readonly IStayService _stayService;
        IUserService userService;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public OfferController(UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IOfferService offerService, IStayService stayService, IUserService userService)
        {
            this.userService = userService;
            _offerService = offerService;
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

        public async Task<IActionResult> Index(OfferFilterViewModel? filter)
        {
            var query = _offerService.GetAll().AsQueryable();
            var filterModel = new OfferFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {

                var model = _offerService.CombinedInclude().Include(x => x.User).Select(x => new OfferViewModel()
                {
                    Id = x.Id,
                    Name= x.Name,
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

            return View(filterModel);
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

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
            /*if (id != null)
            {
                await _offerService.DeleteOfferByIdAsync(id);
                TempData["success"] = "Успешно изтрит запис";
                return RedirectToAction("ListOffers");
            }
            return RedirectToAction("ListOffers");*/
        }
        public async Task<IActionResult> EditOffer(int id)
        {
            //var trav = _offerService.GetOfferByIdAsync(Id);
            //if (trav == null) { return NotFound(); }
            //return View(trav);
            //Offer offers = await _offerService.GetOfferByIdAsync(Id);
            //if (offers == null) { return NotFound(); }
            // View(offers);
            

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
                var imageUploadResult = await cloudService.UploadImageAsync(model.Picture);

        



                Offer offer = new Offer
                {
                    Name = model.Name,
                    Price = model.Price,
                    UserId = user.Id,
                    CoverImage = imageUploadResult,
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
