using Explorers_Haven.Core.IServices;
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
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
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
            var trip = _tripService.GetById(Id);
            if (trip == null) { return NotFound(); }
            return View(trip);
        }

        [HttpPost]
        public IActionResult EditTrip(Trip obj)
        {
            if (ModelState.IsValid)
            {
                _tripService.Update(obj);
                TempData["success"] = "Успешно редактиран запис";
                return View();
            }
            TempData["error"] = "Неуспешна редакция";
            return View();
        }
        public IActionResult AddTrip()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTrip(Trip obj)
        {
            if (ModelState.IsValid)
            {

                _tripService.Add(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListTrips");
            }
            return View();
        }
    }
}
