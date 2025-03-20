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
        [HttpPost]
        public async Task<IActionResult> Rate(int id, int rating)
        {
            // Get the current logged-in user
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            // Fetch the offer from the database
            Offer o = await _offerService.GetOfferByIdAsync(id);

            // Get the existing rating for the current offer and user
            var existingRating = await _ratingService.GetRatingAsync(x => x.OfferId == id && x.UserId == user.Id);

            // Create a new Rating object with the provided rating
            Rating newRating = new Rating()
            {
                OfferId = id,
                Offer = o,
                Stars = rating,
                User = user,
                UserId = user.Id
            };

            if (existingRating != null)
            {
                // If the rating already exists, update its properties
                existingRating.Stars = rating;  // Update the rating stars value
                existingRating.Offer = o;       // Update the offer reference
                existingRating.OfferId = id;    // Update the offer ID if needed

                // Call the update method in your service
                await _ratingService.UpdateRatingAsync(existingRating);
            }
            else
            {
                // If no existing rating, create a new one and add it
                await _ratingService.AddRatingAsync(newRating);
            }

            // Return a success response to the frontend
            return Json(new { success = true, message = "Rating submitted successfully!" });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
