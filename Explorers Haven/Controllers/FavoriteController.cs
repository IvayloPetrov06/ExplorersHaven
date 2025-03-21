using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Favorite;
using Explorers_Haven.ViewModels.Main;
using Explorers_Haven.ViewModels.Offer;
using Explorers_Haven.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class FavoriteController : Controller
    {
        IFavoriteService _FavoriteService;
        IOfferService _offerService;
        IStayService _stayService;
        private readonly UserManager<IdentityUser> _userManager;
        IUserService _userService;


        public FavoriteController(IStayService stayService, IUserService userService, UserManager<IdentityUser> userManager, IOfferService offerService, IFavoriteService FavoriteService)
        {
            _stayService = stayService;
            _FavoriteService = FavoriteService;
            _offerService = offerService;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> Cancel(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Favorite b = await _FavoriteService.GetFavoriteAsync(x => x.Id == id);
            if (b != null)
            {
                await _FavoriteService.DeleteFavoriteAsync(b); TempData["success"] = "Favorite canceled!";
                return RedirectToAction("FavoritesPage", "Favorite");
            }
            TempData["error"] = "Favorite doesn't exist!";
            return RedirectToAction("FavoritesPage", "Favorite");
        }
        [HttpPost]
        public async Task<IActionResult> Favorite(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Offer offer = await _offerService.GetOfferByIdAsync(id);
            var fav = await _FavoriteService.GetFavoriteAsync(x => x.OfferId == id && x.UserId == user.Id);

            if (fav == null)
            {
                // Add new favorite
                Favorite newFavorite = new Favorite()
                {
                    OfferId = id,
                    Offer = offer,
                    User = user,
                    UserId = user.Id
                };

                await _FavoriteService.AddFavoriteAsync(newFavorite);

                return Json(new { success = true, message = "Added to favorites", isFavorited = true });
            }
            else
            {
                // Remove existing favorite
                await _FavoriteService.DeleteFavoriteAsync(fav);

                return Json(new { success = true, message = "Removed from favorites", isFavorited = false });
            }
        }
            [HttpPost]
        public async Task<IActionResult> FavoritesPage(FavoritePageViewModel filter)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);
            var query = _FavoriteService.GetAll().Where(x => x.UserId == user.Id);
            var filterModel = new FavoriteFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {

                var model = _FavoriteService.AllWithInclude().Include(x => x.Offer).Include(x => x.User).Select(x => new FavoriteViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name,
                    OfferCoverImage = x.Offer.CoverImage,
                }).ToList();

                filterModel = new FavoriteFilterViewModel
                {
                    Favorites = model,
                    Search = filter.Search,

                };
            }
            else
            {
                //var tempUsers = await _userService.GetAllUserNamesAsync();
                var tempOffers = await _offerService.GetAllOfferNamesAsync();
                //if (tempUsers.Contains(filter.Search))
                //{
                //    query = query.Where(x => x.User.Username == filter.Search);
                //}
                if (tempOffers.Contains(filter.Search))
                {
                    query = query.Where(x => x.OfferName == filter.Search);
                }

                filterModel = new FavoriteFilterViewModel
                {
                    Favorites = query.Include(x => x.User)
                .Select(x => new FavoriteViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name,
                    OfferCoverImage = x.Offer.CoverImage,
                }).ToList(),
                    Search = filter.Search
                };
            };
            return View(filterModel);
        }
        public async Task<IActionResult> FavoritesPage()
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (tempUser == null)
            {
                return NotFound("No Identity user found.");
            }
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);
            if (user == null)
            {
                return NotFound("No application user found for email: " + tempUser.Email);
            }
            var userFavorites = await _FavoriteService.GetAll().Where(x => x.UserId == user.Id)
        .Include(x => x.Offer)
        .Select(x => new FavoriteViewModel
        {
            Id = x.Id,
            UserId = x.UserId,
            UserName = x.User.Username,
            OfferName = x.Offer.Name,
            OfferCoverImage = x.Offer.CoverImage,
        }).ToListAsync();

            // Create and pass a view model with the user's Favorites
            var filterModel = new FavoriteFilterViewModel
            {
                Favorites = userFavorites,
            };

            return View(filterModel);

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
