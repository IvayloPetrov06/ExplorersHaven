using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Activity;
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

        public async Task<IActionResult> ListActivities()
        {
            IEnumerable<Activity> offers = await _actService.GetAllActivityAsync();
            return View(offers);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != null) 
            {
                await _actService.DeleteActivityByIdAsync(id);
                TempData["success"] = "Успешно изтро събитие";
                return RedirectToAction("ListActivities");
            }
            return RedirectToAction("ListActivities");
        }
        public async Task<IActionResult> EditActivity(int Id)
        {
            Activity act = await _actService.GetActivityByIdAsync(Id);
            if (act == null) { return NotFound(); }
            var trips = _tripService.GetAllTripAsync().Result;
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View(act);
        }
        [HttpPost]
        public async Task<IActionResult> EditActivity(Activity obj)
        {
            if (ModelState.IsValid)
            {
                await _actService.UpdateActivityAsync(obj);
                TempData["success"] = "Успешно редактирано събитие";
                return RedirectToAction("ListActivities");
            }
            else
            {
                TempData["error"] = "Неуспешна редакция";
                return View(obj);
            }
        }
        public async Task<IActionResult> AddActivity()
        {
            var trips = _tripService.GetAll();
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActivity(Activity obj)
        {
            
             await _actService.AddActivityAsync(obj);
             TempData["success"] = "Успешно добавен събитие";
             return RedirectToAction("ListActivities");
            
        }
    }
}
