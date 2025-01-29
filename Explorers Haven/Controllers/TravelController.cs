using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class TravelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITravelogueService _travService;
        private readonly ITripService _tripService;
        public TravelController(ITravelogueService travService, ITripService tripService, ApplicationDbContext context)
        {
            _travService = travService;
            _tripService = tripService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult InsertView2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertView2(Travelogue obj)
        {
            if (obj != null)
            {
                _context.Travelogues.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("ListTravels");
            }
            return View();
        }

        public IActionResult ListTravels()
        {
            var list = _context.Travelogues.ToList();
            return View(list);
        }
        public IActionResult InsertForms()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            var car = _context.Travelogues.FirstOrDefault(x => x.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Travelogues.Remove(car);
            _context.SaveChanges();
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListTravels");
        }
        public IActionResult EditCar(int Id)
        {
            var car = _context.Travelogues.FirstOrDefault(x => x.Id == Id);
            if (car == null) { return NotFound(); }
            return View(car);
        }
        [HttpPost]
        public IActionResult EditCar(Travelogue obj)
        {
            if (ModelState.IsValid)
            {
                _context.Travelogues.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Успешно редактиран запис";
                return View();
                //return RedirectToAction("ListCars");
            }
            TempData["error"] = "Неуспешна редакция";
            return View();
        }
        [HttpPost]
        public IActionResult InsertForms(Travelogue obj)
        {
            if (ModelState.IsValid)
            {
                _context.Travelogues.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListTravels");
            }
            return View();
        }
    }
}
