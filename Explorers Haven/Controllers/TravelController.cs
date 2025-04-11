using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Explorers_Haven.ViewModels.Travel;
using Explorers_Haven.Core.Services;
using Explorers_Haven.ViewModels.Stay;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Explorers_Haven.ViewModels.Offer;

namespace Explorers_Haven.Controllers
{
    public class TravelController : Controller
    {
        private readonly ITravelService _travelService;
        private readonly IOfferService _offerService;
        private readonly ITransportService _trService;
        private readonly UserManager<IdentityUser> userManager;
        IUserService userService;
        public TravelController(IUserService userService,UserManager<IdentityUser> _userManager, ITransportService trService,ITravelService travelService, IOfferService offerService)
        {
            _trService = trService;
            _travelService = travelService;
            _offerService = offerService;
            this.userService = userService;
            userManager = _userManager;

        }

        public async Task<IActionResult> Index(TravelViewModel? filter)
        {

            var query = _travelService.GetAll().AsQueryable();
            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.Start != null)
            {
                query = query.Where(x => x.Start.Contains(filter.Start));
            }
            if (filter.Finish != null)
            {
                query = query.Where(x => x.Finish.Contains(filter.Finish));
            }
            var model = new TravelViewModel
            {
                Id = filter.Id,
                Start = filter.Start,
                Finish = filter.Finish,
                Transport = filter.Transport,
                Travels = query.ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> ListTravels()
        {
            var list = _travelService.GetAll();
            return View(list);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                await _travelService.DeleteTravelByIdAsync(id);
            }
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("AllTravel");
        }

        public async Task<IActionResult> AllTravel(TravelFilterViewModel? filter)
        {
            var query = _travelService.GetAll().AsQueryable();


            if (string.IsNullOrEmpty(filter.Title))
            {
                var model = _travelService.CombinedInclude().Include(x => x.User).ThenInclude(x => x.Travels).Select(x => new TravelViewModel()
                {
                    Id = x.Id,
                    Start = x.Start,
                    Finish = x.Finish,
                    UserName = x.User.Username,
                    Transport = x.Transport.Name,
                    OfferName = x.Offer.Name,
                    Arrival = x.Arrival,
                }).ToList();

                var filterModel = new TravelFilterViewModel
                {
                    Travels = model,

                };
                var sortedList = filterModel.Travels.OrderBy(x => x.Id).ToList();
                filterModel.Travels = sortedList;
                return View(filterModel);
            }
            else
            {
                if (!string.IsNullOrEmpty(filter.Title))
                {
                    query = query.Where(x => x.Offer.Name == filter.Title);
                }


                var filterModel = new TravelFilterViewModel
                {
                    Travels = query.Include(x => x.User).ThenInclude(x => x.Travels).Select(x => new TravelViewModel()
                    {
                        Id = x.Id,
                        Start = x.Start,
                        Finish = x.Finish,
                        UserName = x.User.Username,
                        Transport = x.Transport.Name,
                        OfferName = x.Offer.Name,
                        Arrival = x.Arrival,
                    }).ToList(),
                    Title = filter.Title
                };
                var sortedList = filterModel.Travels.OrderBy(x => x.OfferName).ToList();
                filterModel.Travels = sortedList;
                return View(filterModel);
            }

        }
        public async Task<IActionResult> EditTravel(int Id)
        {
            var transports = _trService.GetAll();
            ViewBag.Transports = new SelectList(transports, "Id", "Name");
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");

            var model = _travelService.GetAll()
                .Where(x => x.Id == Id)
                .Include(x => x.User)
                .Select(x => new EditTravelViewModel()
                {
                    Id = x.Id,
                    Start = x.Start,
                    Finish = x.Finish,
                    DurationDays = x.DurationDays,
                    Arrival = x.Arrival.Value,
                    TransportId = x.TransportId,
                    OfferId = x.OfferId,
                    UserId = x.UserId,
                })
                .FirstOrDefault();

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditTravel(EditTravelViewModel model)
        {
            var tempOf = await _offerService.GetOfferByIdAsync(model.OfferId.Value);
            if (model.DurationDays > (tempOf.DurationDays / 2))
            {
                TempData["error"] = "Продължителността е твърде голяма";
                var transports = _trService.GetAll();
                ViewBag.Transports = new SelectList(transports, "Id", "Name");
                var offers = _offerService.GetAll();
                ViewBag.Offers = new SelectList(offers, "Id", "Name");
                return View(model);
            }
            if (model.TransportId == 0)
            {
                TempData["error"] = "Изберете транспорт";
                var transports = _trService.GetAll();
                ViewBag.Transports = new SelectList(transports, "Id", "Name");
                var offers = _offerService.GetAll();
                ViewBag.Offers = new SelectList(offers, "Id", "Name");
                return View(model);
            }
            if (model.OfferId == 0)
            {
                TempData["error"] = "Изберете оферта";
                var transports = _trService.GetAll();
                ViewBag.Transports = new SelectList(transports, "Id", "Name");
                var offers = _offerService.GetAll();
                ViewBag.Offers = new SelectList(offers, "Id", "Name");
                return View(model);
            }
            var alltrav = await _travelService.GetAllTravelAsync();
            foreach (var travel in alltrav)
            {
                if (travel.OfferId == model.OfferId)
                {
                    if (travel.Id == model.Id && travel.Arrival != model.Arrival)
                    {
                        TempData["error"] = "Посоката на пътуване не може да се променя";
                        var transports = _trService.GetAll();
                        ViewBag.Transports = new SelectList(transports, "Id", "Name");
                        var offers = _offerService.GetAll();
                        ViewBag.Offers = new SelectList(offers, "Id", "Name");
                        return View(model);
                    }
                }
            }
            foreach (var travel in alltrav)
            {
                if (travel.Id == model.Id)
                {
                    if (travel.OfferId != model.OfferId)
                    {
                        TempData["error"] = "Офертата не може да се променя";
                        var transports = _trService.GetAll();
                        ViewBag.Transports = new SelectList(transports, "Id", "Name");
                        var offers = _offerService.GetAll();
                        ViewBag.Offers = new SelectList(offers, "Id", "Name");
                        return View(model);
                    }
                }
            }

                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
            User user = await userService.GetUserAsync(x => x.UserIdentity.Email == tempUser.Email);

            Travel track = _travelService.GetAll().FirstOrDefault(x => x.Id == model.Id);

            track.Start = model.Start;
            track.Finish = model.Finish;
            track.DurationDays = model.DurationDays;
            track.Arrival = model.Arrival;
            track.TransportId = model.TransportId.Value;
            track.OfferId = model.OfferId.Value;
            track.UserId = user.Id;

            await _travelService.UpdateTravelAsync(track);
            TempData["success"] = "Успешно редактиран запис";

            return RedirectToAction("AllTravel");
        }
        public async Task<IActionResult> AddTravel()
        {
            var transports = _trService.GetAll();
            ViewBag.Transports = new SelectList(transports, "Id", "Name");
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTravel(AddTravelViewModel model)
        {
            if (model.OfferId == 0)
            {
                TempData["error"] = "Изберете оферта";
                var transports = _trService.GetAll();
                ViewBag.Transports = new SelectList(transports, "Id", "Name");
                var offers = _offerService.GetAll();
                ViewBag.Offers = new SelectList(offers, "Id", "Name");
                return View(model);
            }
            if (model.TransportId == 0)
            {
                TempData["error"] = "Изберете транспорт";
                var transports = _trService.GetAll();
                ViewBag.Transports = new SelectList(transports, "Id", "Name");
                var offers = _offerService.GetAll();
                ViewBag.Offers = new SelectList(offers, "Id", "Name");
                return View(model);
            }
            var tempOf = await _offerService.GetOfferByIdAsync(model.OfferId.Value);
            if (model.DurationDays > (tempOf.DurationDays%2))
            {
                TempData["error"] = "Продължителността е твърде голяма";
                var transports = _trService.GetAll();
                ViewBag.Transports = new SelectList(transports, "Id", "Name");
                var offers = _offerService.GetAll();
                ViewBag.Offers = new SelectList(offers, "Id", "Name");
                return View(model);
            }
            
            var alltrav = await _travelService.GetAllTravelAsync();
            foreach (var travel in alltrav) 
            {
                if (travel.OfferId == model.OfferId)
                {
                    if (travel.Arrival == model.Arrival)
                    {
                        if (travel.Arrival == true)
                        {
                            TempData["error"] = "Офертата вече има отиване";
                            var transports = _trService.GetAll();
                            ViewBag.Transports = new SelectList(transports, "Id", "Name");
                            var offers = _offerService.GetAll();
                            ViewBag.Offers = new SelectList(offers, "Id", "Name");
                            return View(model);
                        }
                        else
                        {
                            TempData["error"] = "Офертата вече има връщане";
                            var transports = _trService.GetAll();
                            ViewBag.Transports = new SelectList(transports, "Id", "Name");
                            var offers = _offerService.GetAll();
                            ViewBag.Offers = new SelectList(offers, "Id", "Name");
                            return View(model);
                        }
                    }
                }
            }
            
                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                User user = await userService.GetUserAsync(x => x.UserIdentity.Email == tempUser.Email);



                Travel trav = new Travel
                {
                    Start = model.Start,
                    Finish = model.Finish,
                    DurationDays = model.DurationDays,
                    Arrival = model.Arrival,
                    TransportId = model.TransportId.Value,
                    OfferId = model.OfferId.Value,
                    UserId = user.Id,

                };
                await _travelService.AddTravelAsync(trav);
                TempData["success"] = "Успешно добавен запис";
            
            return RedirectToAction("AllTravel");
        }
    }
}
