using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Booking;
using Explorers_Haven.ViewModels.Main;
using Explorers_Haven.ViewModels.Offer;
using Explorers_Haven.ViewModels.User;
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
        IStayService _stayService;
        private readonly UserManager<IdentityUser> _userManager;
        IUserService _userService;


        public BookingController(IStayService stayService,IUserService userService, UserManager<IdentityUser> userManager, IOfferService offerService, IBookingService bookingService)
        {
            _stayService = stayService;
            _bookingService = bookingService;
            _offerService = offerService;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> Cancel(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Booking b = await _bookingService.GetBookingAsync(x => x.Id == id);
            if (b != null)
            {
                await _bookingService.DeleteBookingAsync(b); TempData["success"] = "Booking canceled!";
                return RedirectToAction("BookingsPage", "Booking");
            }
            TempData["error"] = "Booking doesn't exist!";
            return RedirectToAction("BookingsPage", "Booking");
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
        public async Task<IActionResult> Book(int id,decimal ppl, decimal discppl,DateOnly st)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Offer o = await _offerService.GetOfferByIdAsync(id);

            Booking b = new Booking()
            {
                PeopleCount = ppl,
                YoungOldPeopleCount = discppl,
                OfferId = id,
                Offer = o,
                User = user,
                UserId = user.Id,
                OfferName = o.Name
            };
            var tempBook = await _bookingService.GetAllBookingsAsync(x=>x.StartDate==st);
            DateOnly dayss = st.AddDays(o.DurationDays.Value);
            decimal pplCount = 0;
            foreach (var p in tempBook)
            {
                pplCount += p.PeopleCount.Value;
            }
            if ((o.MaxPeople - pplCount) >= ppl)
            {
                if (o.StartDate.Value.DayOfWeek == st.DayOfWeek && o.LastDate.Value >= st)
                {
                    b.StartDate = st;
                }
                else
                {
                    TempData["error"] = "Date is unavailable!";
                    return RedirectToAction("OfferPage", "Home", new { Id = id });
                }
            }
            else
            {
                TempData["error"] = $"Only{o.MaxPeople - pplCount} available spots!";
                return RedirectToAction("OfferPage", "Home", new { Id = id });
            }
            
            
            decimal normppl = ppl - discppl;
            decimal realprice = 0;
            if (o.Discount != null)
            {
                decimal d = (o.Price.Value / 100) * o.Discount.Value;
                decimal disc = (o.Price.Value * discppl) - d;
                realprice = (o.Price.Value * normppl) + disc;
                b.Price = realprice;
            }
            else
            {
                realprice = o.Price.Value * ppl;
                b.Price = realprice;
            }
            await _bookingService.AddBookingAsync(b);
            TempData["success"] = "Offer was booked!";
            return RedirectToAction("BookingsPage", "Booking");
        }
        [HttpPost]
        public async Task<IActionResult> BookingsPage(BookingPageViewModel filter)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);
            var query = _bookingService.GetAll().Where(x=>x.UserId== user.Id);
            var filterModel = new BookingFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {

                var model = _bookingService.AllWithInclude().Include(x => x.Offer).Include(x => x.User).Select(x => new FavoriteViewModel()
                {
                    PeopleCount = x.PeopleCount,
                    YoungOldPeopleCount = x.YoungOldPeopleCount,
                    StartDate = x.StartDate,
                    Price = x.Price,
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name,
                    OfferCoverImage = x.Offer.CoverImage,
                }).ToList();

                filterModel = new BookingFilterViewModel
                {
                    Bookings = model,
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

                filterModel = new BookingFilterViewModel
                {
                    Bookings = query.Include(x => x.User)
                .Select(x => new FavoriteViewModel()
                {
                    PeopleCount = x.PeopleCount,
                    YoungOldPeopleCount = x.YoungOldPeopleCount,
                    StartDate = x.StartDate,
                    Price = x.Price,
                    Id = x.Id,
                    UserId = x.UserId,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name,
                    OfferCoverImage = x.Offer.CoverImage,
                }).ToList(),
                    Search = filter.Search
                };
            };
            var sortedList = query.OrderBy(x => x.Price).ToList();
            filterModel.Cheapest_Bookings = sortedList;

            return View(filterModel);
        }
        public async Task<IActionResult> BookingsPage()
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
            var userBookings = await _bookingService.GetAll().Where(x => x.UserId == user.Id)
        .Include(x => x.Offer) // Including related Offer data for displaying the Offer Name
        .Select(x => new FavoriteViewModel
        {
            PeopleCount = x.PeopleCount,
            YoungOldPeopleCount = x.YoungOldPeopleCount,
            StartDate = x.StartDate,
            Price = x.Price,
            Id = x.Id,
            UserId = x.UserId,
            UserName = x.User.Username,
            OfferName = x.Offer.Name,
            OfferCoverImage=x.Offer.CoverImage,
        }).ToListAsync();

            // Create and pass a view model with the user's bookings
            var filterModel = new BookingFilterViewModel
            {
                Bookings = userBookings,
            };

            return View(filterModel);

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
