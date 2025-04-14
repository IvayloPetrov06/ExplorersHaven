using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Favorite;
using Explorers_Haven.ViewModels.Main;
using Explorers_Haven.ViewModels.Offer;
using Explorers_Haven.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
                await _FavoriteService.DeleteFavoriteAsync(b); TempData["success"] = "Премахнато от любими!";
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
                Favorite newFavorite = new Favorite()
                {
                    OfferId = id,
                    Offer = offer,
                    OfferName=offer.Name,
                    User = user,
                    UserId = user.Id
                };

                await _FavoriteService.AddFavoriteAsync(newFavorite);

                return Json(new { success = true, message = "Добавено към любими", isFavorited = true });
            }
            else
            {
                await _FavoriteService.DeleteFavoriteAsync(fav);

                return Json(new { success = true, message = "Премахнато от любими", isFavorited = false });
            }
        }
        public async Task<IActionResult> FavoritesPage(FavoriteFilterViewModel filter)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);
            var query = _FavoriteService.GetAll().Where(x => x.UserId == user.Id);
           // var filterModel = new FavoriteFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {

                var model = _FavoriteService.AllWithInclude().Include(x => x.Offer).Include(x => x.User).Where(x => x.UserId == user.Id).Select(x => new FavoriteViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name,
                    OfferCoverImage = x.Offer.CoverImage,
                    Price = x.Offer.Price
                }).ToList();

                var filterModel = new FavoriteFilterViewModel
                {
                    Favorites = model,
                    Search = filter.Search,

                };
                return View(filterModel);
            }
            else
            {
                
                if (!string.IsNullOrEmpty(filter.Search))
                {
                    query = query.Where(x => x.OfferName.ToLower() == filter.Search.ToLower());
                }

                var filterModel = new FavoriteFilterViewModel
                {
                    Favorites = query.Include(x => x.User).Where(x => x.UserId == user.Id)
                .Select(x => new FavoriteViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name,
                    OfferCoverImage = x.Offer.CoverImage,
                    Price = x.Offer.Price
                }).ToList(),
                    Search = filter.Search
                };
                return View(filterModel);
            };
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
