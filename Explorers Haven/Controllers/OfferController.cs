using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class OfferController : Controller
    {
        
        private readonly IOfferService _offerService;
        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public IActionResult Index(OfferViewModel? filter)
        {
            
            var query = _offerService.GetAll().AsQueryable();
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
            var model = new OfferViewModel
            {
                Id = filter.Id,
                Name = filter.Name,

                MinPrice = filter.MinPrice,
                MaxPrice = filter.MaxPrice,
                Offers = query.ToList()
            };

            return View(model);
        }

        public IActionResult ListOffers()
        {
            var list = _offerService.GetAllOfferAsync();
            return View(list);
        }
       
        public IActionResult Delete(int id)
        {
            _offerService.DeleteOfferByIdAsync(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListOffers");
        }
        public IActionResult EditOffer(int Id)
        {
            var trav = _offerService.GetOfferByIdAsync(Id);
            if (trav == null) { return NotFound(); }
            return View(trav);
        }
        [HttpPost]
        public IActionResult EditOffer(Offer obj)
        {
            if (ModelState.IsValid)
            {
                _offerService.UpdateOfferAsync(obj);
                TempData["success"] = "Успешно редактиран запис";
                return RedirectToAction("ListOffers");
            }
            TempData["error"] = "Неуспешна редакция";
            return View();
        }
        public IActionResult AddOffer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddOffer(Offer obj)
        {
            if (ModelState.IsValid)
            {

                _offerService.AddOfferAsync(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListOffers");
            }
            return View();
        }
    }
}
