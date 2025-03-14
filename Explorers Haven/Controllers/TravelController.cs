using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Explorers_Haven.ViewModels.Travel;
using Explorers_Haven.Core.Services;

namespace Explorers_Haven.Controllers
{
    public class TravelController : Controller
    {
        private readonly ITravelService _travelService;
        private readonly IOfferService _offerService;
        public TravelController(ITravelService travelService, IOfferService offerService)
        {
            _travelService = travelService;
            _offerService = offerService;

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
            /*if (filter.Transport != null)
            {
                query = query.Where(x => x.Transport.Contains(filter.Transport));
            }*/
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
            await _travelService.DeleteTravelByIdAsync(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListTravels");
        }
        public async Task<IActionResult> EditTravel(int Id)
        {
            var tr = _travelService.GetTravelByIdAsync(Id);
            if (tr == null) { return NotFound(); }
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View(tr);

        }
        [HttpPost]
        public async Task<IActionResult> EditTravel(Travel obj)
        {
            if (ModelState.IsValid)
            {
                await _travelService.UpdateTravelAsync(obj);
                TempData["success"] = "Успешно редактирано събитие";
                return RedirectToAction("ListActivities");
            }
            else
            {
                TempData["error"] = "Неуспешна редакция";
                return View(obj);
            }
        }
        public async Task<IActionResult> AddTravel()
        {
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTravel(Travel obj)
        {
            await _travelService.AddTravelAsync(obj);
            TempData["success"] = "Успешно добавен запис";
            return RedirectToAction("ListTravels");
        }
    }
}
