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
        private readonly IStayService _stayService;
        IUserService userService;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public HomeController(UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IStayService stayService,IOfferService offerService, IUserService userService)
        {
            _offerService = offerService;
            this.userService = userService;
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
            if (tempOffer.StayId != null)
            {
                var tempStay = await _stayService.GetStayByIdAsync(tempOffer.StayId.Value);
                var model = _offerService.GetAll().Where(x => x.Id == id).Include(x => x.User)
                .Select(x => new OfferPageViewModel()
                {
                    Id = tempOffer.Id,
                    OfferCover = x.CoverImage,
                    Price = x.Price,
                    Name = x.Name,
                    UserId = x.UserId,
                    StayName = tempStay.Name
                }).FirstOrDefault();

                return View(model);
            }
            else
            {
                var model = _offerService.GetAll().Where(x => x.Id == id).Include(x => x.User)
                    .Select(x => new OfferPageViewModel()
                    {
                        Id = tempOffer.Id,
                        OfferCover = x.CoverImage,
                        Price = x.Price,
                        Name = x.Name,
                        UserId = x.UserId
                    }).FirstOrDefault();

                return View(model);
            }

            
        }
        [HttpPost]
        public async Task<IActionResult> OfferPage(int id, OfferPageViewModel model)//copied from edit offer
        {
            return View(model);
        }
    }
}
