using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Controllers
{
    public class TravelController : Controller
    {
        private readonly ITravelService _travelService;
        private readonly ITripService _tripService;
        public TravelController(ITravelService travelService, ITripService tripService)
        {
            _travelService = travelService;
            _tripService = tripService;

        }

        public IActionResult Index(TravelViewModel? filter)
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
            if (filter.Transport != null)
            {
                query = query.Where(x => x.Transport.Contains(filter.Transport));
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

        public IActionResult ListTravels()
        {
            var list = _travelService.GetAll();
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            _travelService.Delete(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListTravels");
        }
        public IActionResult EditTravel(int Id)
        {
            var tr = _travelService.GetById(Id);
            if (tr == null) { return NotFound(); }
            var trips = _tripService.GetAll();
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View(tr);
        }
        [HttpPost]
        public IActionResult EditTravel(Travel obj)
        {
            if (_tripService.GetById(obj.TripId).Travel == null)
            {
                _travelService.Update(obj);
                TempData["success"] = "Успешно редактиран запис";
                return RedirectToAction("ListTravels");
            }
            TempData["error"] = "Every trip can have only one travel";
            return View();
        }
        public IActionResult AddTravel()
        {
            var trips = _tripService.GetAll();
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddTravel(Travel obj)
        {
            if (_tripService.GetById(obj.TripId).Travel==null) 
            {
                _travelService.Add(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListTravels");
            }
            TempData["error"] = "Every trip can have only one travel";
            return View();
            
        }
    }
}
