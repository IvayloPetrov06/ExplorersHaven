using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Booking;
using Explorers_Haven.ViewModels.Main;
using Explorers_Haven.ViewModels.Offer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;

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
                await _bookingService.DeleteBookingAsync(b); TempData["success"] = "Booking canceled!";
                return RedirectToAction("HomePage", "Home");
            }
            TempData["error"] = "Booking doesn't exist!";
            return RedirectToAction("OfferPage", "Home");
        }
        /*offer:
         * public int? MaxPeople { get; set; }
        public int? Discount { get; set; }
        public int? DurationDays { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? LastDate { get; set; }
        booking:
         public int? PeopleCount {  get; set; }
        public int? YoungOldPeopleCount { get; set; }
        public DateOnly? StartDate { get; set; }*/
        public async Task<IActionResult> Book(int id,int ppl,int discppl,DateOnly st)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Offer o = await _offerService.GetOfferByIdAsync(id);

            Booking b = new Booking()
            {
                PeopleCount = ppl,
                YoungOldPeopleCount = discppl,
                StartDate = st,
                OfferId = id,
                Offer = o,
                User = user,
                UserId = user.Id
            };
            await _bookingService.AddBookingAsync(b);
            TempData["success"] = "Offer was booked!";
            return RedirectToAction("HomePage", "Home");
        }
        /*public async Task<IActionResult> BookingsPage(BookingPageViewModel filter)
        {
            var query = _bookingService.GetAll().AsQueryable();
            var filterModel = new BookingFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {

                var model = _bookingService.AllWithInclude().Include(x => x.Offer).Include(x => x.User).Select(x => new BookingViewModel()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name
                }).ToList();

                filterModel = new BookingFilterViewModel
                {
                    Bookings = model,
                    Search = filter.Search,

                };
            }
            else
            {
                var tempUsers = await _userService.GetAllUserNamesAsync();
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
        }*/
        public IActionResult Index()
        {
            return View();
        }
    }
}
