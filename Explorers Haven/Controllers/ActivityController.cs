using System.Configuration;
using System.Runtime.InteropServices;
using CloudinaryDotNet;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Activity;
using Explorers_Haven.ViewModels.Offer;
using Explorers_Haven.ViewModels.Travel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _actService;
        private readonly IOfferService _offerService;
        private readonly UserManager<IdentityUser> userManager;
        IUserService userService;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public ActivityController(IConfiguration configuration, CloudinaryService cloud, IUserService userService, UserManager<IdentityUser> _userManager, IActivityService actService, IOfferService offerService)
        {
            _actService = actService;
            _offerService = offerService;
            this.userService = userService;
            userManager = _userManager;

            this.cloudService = cloud;

            _configuration = configuration;
            var account = new Account(
           _configuration["Cloudinary:CloudName"],
           _configuration["Cloudinary:ApiKey"],
           _configuration["Cloudinary:ApiSecret"]
             );
            _cloudinary = new Cloudinary(account);

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
                return RedirectToAction("AllActivity");
            }
            return RedirectToAction("AllActivity");
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

            var model = _actService.GetAll()
                .Where(x => x.Id == Id)
                .Include(x => x.User)
                .Select(x => new EditActivityViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    CoverImage = x.CoverImage,
                    OfferId = x.OfferId,
                })
                .FirstOrDefault();
            var offers = _offerService.GetAllOfferAsync().Result;
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditActivity(EditActivityViewModel model)
        {
            if (model.OfferId == 0)
            {
                TempData["error"] = "Изберете оферта";
                var stays = _offerService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }

            var tempac = await _actService.GetActivityAsync(x => x.Id == model.Id);
            if (tempac != null)
            {

                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                User user = await userService.GetUserAsync(x => x.Email == tempUser.Email);

                Activity track = _actService.GetAll().FirstOrDefault(x => x.Id == model.Id);
                if (track == null)
                    return NotFound();


                if (model.Picture != null)
                {
                    var imageUploadResult = await cloudService.UploadImageAsync(model.Picture);
                    track.CoverImage = imageUploadResult;
                }



                track.OfferId = model.OfferId.Value;
                track.Name = model.Name;
                track.UserId = user.Id;



                await _actService.UpdateActivityAsync(track);
                TempData["success"] = "Успешно редактиран запис";
            }
            else
            {
                TempData["error"] = "Това място за престой не съществува";
            }
            return RedirectToAction("AllActivity");
        }
        public async Task<IActionResult> AddActivity()
        {
            var offers = _offerService.GetAll();
            ViewBag.Offers = new SelectList(offers, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActivity(AddActivityViewModel model)
        {
            if (model.OfferId == 0)
            {
                TempData["error"] = "Изберете оферта";
                var stays = _offerService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }
            var tempStay = await _actService.GetActivityAsync(x => x.Name == model.Name);
            if (tempStay == null)
            {
                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                User user = await userService.GetUserAsync(x => x.UserIdentity.Email == tempUser.Email);

                var imageUploadResult = await cloudService.UploadImageAsync(model.Picture);

                Console.WriteLine($"Found user in userService: {user.Email}");


                Activity offer = new Activity
                {
                    Name = model.Name,

                    OfferId = model.OfferId.Value,
                    UserId = user.Id,
                };

                if (model.Picture != null )
                {

                    var imageUploadResult1 = await cloudService.UploadImageAsync(model.Picture);
                    offer.CoverImage = imageUploadResult1;
                }
                else
                {
                    offer.CoverImage = "/Images/missing.jpg";
                }
                await _actService.AddActivityAsync(offer);
                TempData["success"] = "Успешно добавяне";
                return RedirectToAction("AllActivity");
            }
            else
            {
                return View(model);
            };
            
        }
    }
}
