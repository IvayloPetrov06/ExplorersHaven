using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Activity;
using Explorers_Haven.ViewModels.Travel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _actService;
        private readonly IOfferService _offerService;
        public ActivityController(IActivityService actService, IOfferService offerService)
        {
            _actService = actService;
            _offerService = offerService;

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
                return RedirectToAction("AllActivities");
            }
            return RedirectToAction("AllActivities");
        }

        public async Task<IActionResult> AllActivity(ActivityFilterViewModel? filter)
        {
            var query = _actService.GetAll().AsQueryable();
            //var playlists = await playlistService.GetAllPlaylistsAsync();


            if (string.IsNullOrEmpty(filter.Title))
            {
                var model = _actService.CombinedInclude().Include(x => x.User).ThenInclude(x => x.Activities).Select(x => new ActivityViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserName = x.User.Username,
                    OfferName = x.Offer.Name,
                    ImageLink=x.CoverImage
                }).ToList();

                var filterModel = new ActivityFilterViewModel
                {
                    Activities = model,

                };
                var sortedList = filterModel.Activities.OrderBy(x => x.OfferName).ToList();
                filterModel.Activities = sortedList;
                return View(filterModel);
            }
            else
            {
                if (!string.IsNullOrEmpty(filter.Title))
                {
                    query = query.Where(x => x.Offer.Name == filter.Title);
                }


                var filterModel = new ActivityFilterViewModel
                {
                    Activities = query.Include(x => x.User).ThenInclude(x => x.Activities).Select(x => new ActivityViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UserName = x.User.Username,
                        OfferName = x.Offer.Name,
                        ImageLink = x.CoverImage
                    }).ToList(),
                    Title = filter.Title
                };
                var sortedList = filterModel.Activities.OrderBy(x => x.OfferName).ToList();
                filterModel.Activities = sortedList;
                return View(filterModel);
            }

        }

        public async Task<IActionResult> EditActivity(int Id)
        {
            Activity act = await _actService.GetActivityByIdAsync(Id);
            if (act == null) { return NotFound(); }
            var offers = _offerService.GetAllOfferAsync().Result;
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
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
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
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
