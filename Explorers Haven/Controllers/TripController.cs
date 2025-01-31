using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.DataAccess;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripService _tripService;
        private readonly ITravelogueService _travService;
        public TripController(ITripService tripService, ITravelogueService travService)
        {
            _tripService = tripService;
            _travService = travService;

        }

        public IActionResult Index(TripViewModel? filter)
        {

            var query = _tripService.GetAll().AsQueryable();
            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            var model = new TripViewModel
            {
                Id = filter.Id,
                Name = filter.Name,
                Trips = query.Include(x => x.Name).ToList()
            };

            return View(model);
        }

        public IActionResult ListTrips()
        {
            var list = _tripService.GetAll();
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            _tripService.Delete(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListTrips");
        }
        public IActionResult EditTrip(int Id)
        {
            var trav = _tripService.GetById(Id);
            if (trav == null) { return NotFound(); }
            var travelogues = _travService.GetAll();
            ViewBag.Travelogues = new SelectList(travelogues, "Id", "Name");
            return View(trav);
        }
        [HttpPost]
        public IActionResult EditTrip(Trip obj)
        {
            _tripService.Update(obj);
            TempData["success"] = "Успешно редактиран запис";
            return RedirectToAction("ListTrips");
        }
        public IActionResult AddTrip()
        {
            var travelogues = _travService.GetAll();
            ViewBag.Travelogues = new SelectList(travelogues,"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddTrip(Trip obj)
        {
                _tripService.Add(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListTrips");
            //TempData["error"] = "Неуспешно добавяне";
            //return View();
        }
    }
}
