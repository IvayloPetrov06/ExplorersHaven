using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.Controllers
{
    public class BookingController : Controller
    {
        IBookingService _bookingService;
        IOfferService _offerService;
        private readonly UserManager<IdentityUser> _userManager;
        IUserService _userService;


        public BookingController(IUserService userService, UserManager<IdentityUser> userManager, IOfferService offerService, IBookingService bookingService)
        {
            _bookingService = bookingService;
            _offerService = offerService;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> Cancel(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Booking b = await _bookingService.GetBookingAsync(x => x.OfferId == id && x.UserId == user.Id);
            if (b != null)
            {
                await _bookingService.DeleteBookingAsync(b);
            }
            return RedirectToAction("HomePage", "Home");
        }
        public async Task<IActionResult> Book(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Offer o = await _offerService.GetOfferByIdAsync(id);

            Booking b = new Booking()
            {
                OfferId = id,
                Offer = o,
                User = user,
                UserId = user.Id
            };
            await _bookingService.AddBookingAsync(b);
            return RedirectToAction("HomePage", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
