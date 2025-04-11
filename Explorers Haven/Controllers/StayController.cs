using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using CloudinaryDotNet;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Offer;
using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Explorers_Haven.Controllers
{
    public class StayController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        IUserService userService;
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        private readonly IStayService _stayService;
        private readonly IOfferService _offerService;
        private readonly IAmenityService _amService;
        private readonly IStayAmenityService _saService;
        public StayController(IStayAmenityService saService,IAmenityService amService, UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud,IStayService stayService, IOfferService offerService, IUserService userService)
        {
            _saService = saService;
            _amService = amService;
            _stayService = stayService;
            this.userService = userService;
            _offerService = offerService;
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
            
            if (id != null)
            {
                await _offerService.DeleteAllOffersByStays(id);
                await _stayService.DeleteStayByIdAsync(id);
            }
            TempData["success"] = "Успешно изтрит запис";
            return RedirectToAction("AllStay");
        }
        public async Task<IActionResult> EditStay(int Id)
        {

            var model = _stayService.GetAll()
                .Where(x => x.Id == Id)
                .Include(x => x.User)
                .Select(x => new EditStayViewModel()
                {
                    Id = x.Id,
                    Title = x.Name,
                    ImageUrl = x.Image,
                    Disc = x.Disc,
                    Price = x.Price.Value,
                    Stars = x.Stars.Value,
                    UserId = x.UserId,
                    UserList = new SelectList(userService.GetAll(), "Id", "Username"),
                })
                .FirstOrDefault();

            var sams = _saService.GetAll();
            List<int> list = new List<int>();
            foreach (var sam in sams)
            {
                if (sam.StayId == Id)
                {
                    list.Add(sam.AmenityId.Value);
                }
            }
            model.SelectedAmenities = list.ToArray();
            var ams = _amService.GetAll();

            model.Amenities = ams.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();//ok promenliva
            model.existingAmentities = ams.ToArray();//ok vsichki amenity



            if (model == null)
            {
                return NotFound(); 
            }

            return View(model);
        

        }
        [HttpPost]
        public async Task<IActionResult> EditStay(EditStayViewModel model)
        {


            var tempStay = await _stayService.GetStayAsync(x => x.Id == model.Id);
            if (tempStay != null)
            {

                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                User user = await userService.GetUserAsync(x => x.Email == tempUser.Email);

                Stay track = _stayService.GetAll().FirstOrDefault(x => x.Id == model.Id);
                if (track == null)
                    return NotFound();

                
                if (model.ImageFile != null)
                {
                    var imageUploadResult = await cloudService.UploadImageAsync(model.ImageFile);
                    track.Image = imageUploadResult;
                }
                else
                {
                    track.Image = model.ImageUrl;
                }

                if (model.SelectedAmenities != null)
                {
                    var ams = _amService.GetAll().ToList();

                    foreach (var item in ams)
                    {
                        foreach (var a in model.SelectedAmenities)
                        {
                            if (item.Id == a)
                            {
                                var tempSA = await _saService.GetStayAmenityAsync(x=>x.AmenityId==a && x.StayId == track.Id);
                                if (tempSA == null)
                                {
                                    StayAmenity sa = new StayAmenity()
                                    {
                                        AmenityId = item.Id,
                                        StayId = track.Id,
                                    };
                                    await _saService.AddStayAmenityAsync(sa);
                                }
                            }
                        }
                    }
                    var sams = _saService.GetAll().ToList();
                    foreach (var item in sams)
                    {
                        if (item.StayId == track.Id)
                        {
                            bool has = false;
                            foreach (var b in model.SelectedAmenities)
                            {
                                if (item.AmenityId == b)
                                {
                                    has = true;
                                }
                            }
                            if (has == false)
                            {
                                await _saService.DeleteStayAmenityAsync(item);
                            }

                        }
                    }
                }
                else 
                {
                    var sams = _saService.GetAll().ToList();
                    foreach (var item in sams)
                    {
                        if (item.StayId == track.Id)
                        {
                            await _saService.DeleteStayAmenityAsync(item);
                        }
                    }
                }

                track.Name = model.Title;
                track.UserId = user.Id;
                track.Disc = model.Disc;
                track.Price = model.Price;
                track.Stars = model.Stars;



                await _stayService.UpdateStayAsync(track);
                TempData["success"] = "Успешно редактиран запис";
            }
            else 
            {
                TempData["error"] = "Това място за престой не съществува";
            }
            return RedirectToAction("AllStay");

        }
        public async Task<IActionResult> AddStay()
        {
            var ams = _amService.GetAll();
            var viewModel = new AddStayViewModel
            {
                Amenities = ams.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList(),
                existingAmentities = ams.ToArray(),

            };

            return View(viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> AddStay(AddStayViewModel model)
        {
            var tempStay = await _stayService.GetStayAsync(x => x.Name == model.Title);
            if (tempStay == null)
            {
                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                User user = await userService.GetUserAsync(x => x.Email == tempUser.Email);

                var imageUploadResult = await cloudService.UploadImageAsync(model.imageFile);

                

                Stay track = new Stay()
                {
                    Name = model.Title,
                    UserId = user.Id,
                    Disc = model.Disc,
                    Price = decimal.Parse(model.Price),
                    Stars = model.Stars,
                    Image = imageUploadResult
                };
                await _stayService.AddStayAsync(track);
                


                if (model.SelectedAmenities != null)
                {
                    var ams = _amService.GetAll().ToList();

                    foreach (var item in ams)
                    {
                        foreach (var a in model.SelectedAmenities)
                        {
                            if (item.Id == a)
                            {
                                StayAmenity sa = new StayAmenity()
                                {
                                    AmenityId = item.Id,
                                    StayId = track.Id,
                                };
                                await _saService.AddStayAmenityAsync(sa);
                            }
                        }
                    }
                }
                return RedirectToAction("AllStay");
            }
            else
            {
                TempData["error"] = "Това място за престой вече съществува";
                return RedirectToAction("AllStay");
            }
        }
    }
}
