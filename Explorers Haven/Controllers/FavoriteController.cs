using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.Controllers
{
    public class FavoriteController : Controller
    {
        IFavoriteService _favoriteService;
        IOfferService _offerService;
        private readonly UserManager<IdentityUser> _userManager;
        IUserService _userService;


        public FavoriteController(IUserService userService, UserManager<IdentityUser> userManager, IOfferService offerService, IFavoriteService FavoriteService)
        {
            _favoriteService = FavoriteService;
            _offerService = offerService;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Favorite b = await _favoriteService.GetFavoriteAsync(x => x.OfferId == id && x.UserId == user.Id);
            if (b != null)
            {
                await _favoriteService.DeleteFavoriteAsync(b); TempData["success"] = "Favorite canceled!";
                return RedirectToAction("HomePage", "Home");
            }
            TempData["error"] = "Favorite doesn't exist!";
            return RedirectToAction("OfferPage", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Favorite(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Offer offer = await _offerService.GetOfferByIdAsync(id);
            var fav = await _favoriteService.GetFavoriteAsync(x => x.OfferId == id && x.UserId == user.Id);

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

                await _favoriteService.AddFavoriteAsync(newFavorite);

                return Json(new { success = true, message = "Added to favorites", isFavorited = true });
            }
            else
            {
                // Remove existing favorite
                await _favoriteService.DeleteFavoriteAsync(fav);

                return Json(new { success = true, message = "Removed from favorites", isFavorited = false });
            }
        }
    }
}
