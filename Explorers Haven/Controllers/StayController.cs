﻿using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Controllers
{
    public class StayController : Controller
    {
        private readonly IStayService _stayService;
        private readonly ITripService _tripService;
        public StayController(IStayService stayService, ITripService tripService)
        {
            _stayService = stayService;
            _tripService = tripService;

        }

        public IActionResult Index(StayViewModel? filter)
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

        public IActionResult ListStays()
        {
            var list = _stayService.GetAll();
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            _stayService.DeleteStayByIdAsync(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListStays");
        }
        public IActionResult EditStay(int Id)
        {
            var trav = _stayService.GetStayByIdAsync(Id);
            if (trav == null) { return NotFound(); }
            var trips = _tripService.GetAll();
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View(trav);
        }
        [HttpPost]
        public IActionResult EditStay(Stay obj)
        {
            _stayService.UpdateStayAsync(obj);
            TempData["success"] = "Успешно редактиран запис";
            return RedirectToAction("ListStays");
        }
        public IActionResult AddStay()
        {
            var trips = _tripService.GetAll();
            ViewBag.Trips = new SelectList(trips, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddStay(Stay obj)
        {
            _stayService.AddStayAsync(obj);
            TempData["success"] = "Успешно добавен запис";
            return RedirectToAction("ListStays");
        }
    }
}
