using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _actService;
        private readonly ITripService _tripService;
        public ActivityController(IActivityService actService, ITripService tripService)
        {
            _actService = actService;
            _tripService = tripService;

        }

        public IActionResult Index(ActivityViewModel? filter)
        {

            var query = _actService.GetAll().AsQueryable();
            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            var model = new ActivityViewModel
            {
                Id = filter.Id,
                Name = filter.Name,
                Activities = query.ToList()
            };

            return View(model);
        }

        public IActionResult ListActivities()
        {
            var list = _actService.GetAll();
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            _actService.Delete(id);
            TempData["success"] = "Успешно изтро събитие";
            return RedirectToAction("ListActivities");
        }
        public IActionResult EditActivity(int Id)
        {
            var trip1 = _actService.GetById(Id);
            if (trip1 == null) { return NotFound(); }
            var trips = _tripService.GetAll();
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View(trip1);
        }
        [HttpPost]
        public IActionResult EditActivity(Models.Activity obj)
        {
            _actService.Update(obj);
            TempData["success"] = "Успешно редактирано събитие";
            return RedirectToAction("ListActivities");
        }
        public IActionResult AddActivity()
        {
            var trips = _tripService.GetAll();
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddActivity(Models.Activity obj)
        {
            _actService.Add(obj);
            TempData["success"] = "Успешно добавен събитие";
            return RedirectToAction("ListActivities");
        }
    }
}
