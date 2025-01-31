using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class TravelogueController : Controller
    {
        
        private readonly ITravelogueService _travService;
        public TravelogueController(ITravelogueService travService)
        {
            _travService = travService;
        }

        public IActionResult Index(TravelogueViewModel? filter)
        {
            
            var query = _travService.GetAll().AsQueryable();
            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.MinPrice != null)
            {
                query = query.Where(x => x.Price >= filter.MinPrice.Value);
            }
            if (filter.MaxPrice != null)
            {
                query = query.Where(x => x.Price <= filter.MaxPrice.Value);
            }
            if (filter.Name!=null) 
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            var model = new TravelogueViewModel
            {
                Id = filter.Id,
                MinPrice = filter.MinPrice,
                MaxPrice = filter.MaxPrice,
                Name = filter.Name,
                Travelogues = query.ToList()
            };

            return View(model);
        }

        public IActionResult ListTravelogues()
        {
            var list = _travService.GetAll();
            return View(list);
        }
       
        public IActionResult Delete(int id)
        {
            _travService.Delete(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListTravelogues");
        }
        public IActionResult EditTravelogue(int Id)
        {
            var trav = _travService.GetById(Id);
            if (trav == null) { return NotFound(); }
            return View(trav);
        }
        [HttpPost]
        public IActionResult EditTravelogue(Travelogue obj)
        {
            if (ModelState.IsValid)
            {
                _travService.Update(obj);
                TempData["success"] = "Успешно редактиран запис";
                return RedirectToAction("ListTravelogues");
            }
            TempData["error"] = "Неуспешна редакция";
            return View();
        }
        public IActionResult AddTravelogue()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddTravelogue(Travelogue obj)
        {
            if (ModelState.IsValid)
            {
                
                _travService.Add(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListTravelogues");
            }
            return View();
        }
    }
}
