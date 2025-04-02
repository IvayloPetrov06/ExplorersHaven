using System.Runtime.InteropServices;
using CloudinaryDotNet;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class StayController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        IUserService userService;
        private readonly Cloudinary _cloudinary;
        CloudinaryService cloudService;
        private readonly IStayService _stayService;
        public StayController(UserManager<IdentityUser> _userManager, CloudinaryService cloud,IStayService stayService, IUserService userService)
        {
            userManager = _userManager;
            this.cloudService = cloud;
            _stayService = stayService;
            this.userService = userService;

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
        public async Task<IActionResult> AllStay(StayFilterViewModel? filter)
        {
            var query = _stayService.GetAll().AsQueryable();
            //var playlists = await playlistService.GetAllPlaylistsAsync();


            if (filter.StarValue == null && string.IsNullOrEmpty(filter.Title))
            {
                var model = _stayService.CombinedInclude().Include(x => x.User).ThenInclude(x => x.Stays).Select(x => new StayViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserName = x.User.Username,
                    Stars = x.Stars,
                    Price = x.Price,
                    ImageLink = x.Image
                }).ToList();

                var filterModel = new StayFilterViewModel
                {
                    Stays = model,
                    //Genres = new SelectList(genreService.GetAll(), "Id", "Name"),
                    //Playlists = playlists.ToList()

                };
                return View(filterModel);
            }
            else
            {
                if (filter.StarValue != null)
                {
                    query = query.Where(x => x.Stars == filter.StarValue);
                }
                if (!string.IsNullOrEmpty(filter.Title))
                {
                    query = query.Where(x => x.Name == filter.Title);
                }


                var filterModel = new StayFilterViewModel
                {
                    Stays = query.Include(x => x.User).ThenInclude(x => x.Stays).Select(x => new StayViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UserName = x.User.Username,
                        Stars = x.Stars,
                        Price = x.Price,
                        ImageLink = x.Image
                    }).ToList(),
                    Title = filter.Title,
                    StarValue = filter.StarValue,
                };

                return View(filterModel);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            await _stayService.DeleteStayByIdAsync(id);
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("ListStays");
        }
        public async Task<IActionResult> EditStay(int Id)
        {
            var trav = await _stayService.GetStayByIdAsync(Id);
            if (trav == null) { return NotFound(); }
            //var offers = _offerService.GetAll();
            //ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View(trav);
        }
        [HttpPost]
        public async Task<IActionResult> EditStay(Stay obj)
        {
            var tempStay = await _stayService.GetStayAsync(x => x.Name == obj.Name);
            if (tempStay==null)
            {
                await _stayService.UpdateStayAsync(obj);
                TempData["success"] = "Успешно редактиран запис";
                return RedirectToAction("ListStays");
            }
            TempData["error"] = "Това място за престой вече съществува";
            return View();
        }
        public async Task<IActionResult> AddStay()
        {
            //var offers = _offerService.GetAll();
            //ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStay(Stay obj)
        {
            //var tempOffer = await _offerService.GetOfferByIdAsync(obj.OfferId);
            var tempStay = await _stayService.GetStayAsync(x => x.Name == obj.Name);
            if (tempStay == null)
            {
                await _stayService.AddStayAsync(obj);
                TempData["success"] = "Успешно добавен запис";
                return RedirectToAction("ListStays");
            }
            TempData["error"] = "Това място за престой вече съществува";
            return RedirectToAction("ListStays");
            //return View();
        }
    }
}
