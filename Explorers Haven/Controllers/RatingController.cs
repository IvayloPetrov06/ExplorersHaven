using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.Controllers
{
    public class RatingController : Controller
    {
        IRatingService _ratingService;
        IOfferService _offerService;
        private readonly UserManager<IdentityUser> _userManager;
        IUserService _userService;


        public RatingController(IUserService userService, UserManager<IdentityUser> userManager, IOfferService offerService, IRatingService ratingService)
        {
            _ratingService = ratingService;
            _offerService = offerService;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> Cancel(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Rating b = await _ratingService.GetRatingAsync(x => x.OfferId == id && x.UserId == user.Id);
            if (b != null)
            {
                await _ratingService.DeleteRatingAsync(b); TempData["success"] = "Rating canceled!";
                return RedirectToAction("HomePage", "Home");
            }
            TempData["error"] = "Rating doesn't exist!";
            return RedirectToAction("OfferPage", "Home");
        }
        public async Task<IActionResult> Rate(int id,int rating)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Offer o = await _offerService.GetOfferByIdAsync(id);
            //var tempRating = await
            
            Rating b = new Rating()
            {
                OfferId = id,
                Offer = o,
                Stars = rating,
                User = user,
                UserId = user.Id
            };
            await _ratingService.AddRatingAsync(b);
            TempData["success"] = "Offer was rated!";
            var data = new { success = "Offer was rated!"};
            return Json(data);
            //return RedirectToAction("OfferPage", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
