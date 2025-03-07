using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Controllers
{
    public class StayController : Controller
    {
        private readonly IStayService _stayService;
        private readonly IOfferService _offerService;
        public StayController(IStayService stayService, IOfferService offerService)
        {
            _stayService = stayService;
            _offerService = offerService;

        }

        public async Task<IActionResult> Index(StayViewModel? filter)
        {

            var query = _stayService.GetAll().AsQueryable();
            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (filter.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            var model = new StayViewModel
            {
                Id = filter.Id,
                Name = filter.Name,
                Stays = query.ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> ListStays()
        {
            var list = _stayService.GetAll();
            return View(list);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _stayService.DeleteStayByIdAsync(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListStays");
        }
        public async Task<IActionResult> EditStay(int Id)
        {
            var trav = _stayService.GetStayByIdAsync(Id);
            if (trav == null) { return NotFound(); }
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View(trav);
        }
        [HttpPost]
        public async Task<IActionResult> EditStay(Stay obj)
        {
            if (_offerService.GetOfferByIdAsync(obj.OfferId).Result.Stay == null)
            {
                await _stayService.AddStayAsync(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListStays");
            }
            TempData["error"] = "Every offer can have only one stay";
            return View();
        }
        public async Task<IActionResult> AddStay()
        {
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStay(Stay obj)
        {
            var tempOffer = await _offerService.GetOfferByIdAsync(obj.OfferId);
            var tempStay =await _stayService.GetStayAsync(x => x.OfferId == tempOffer.Id);
            if (tempStay == null)
            {
                await _stayService.AddStayAsync(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListStays");
            }
            TempData["error"] = "Every offer can have only one stay";
            return RedirectToAction("ListStays");
            //return View();
        }
    }
}
