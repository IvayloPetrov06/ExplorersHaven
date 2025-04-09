using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.DataAccess;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Activity;
using Explorers_Haven.ViewModels.Offer;
using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class OfferController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IOfferService _offerService;
        private readonly ITravelService _travelService;
        private readonly IActivityService _activityService;
        private readonly IFavoriteService _favoriteService;
        private readonly IBookingService _bookingService;
        private readonly IStayService _stayService;
        private readonly ICommentService _commentService;
        IUserService userService;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;
        public OfferController(IActivityService activityService, IBookingService bookingService,IFavoriteService favoriteService, ITravelService travelService, ICommentService commentService,UserManager<IdentityUser> _userManager, IConfiguration configuration, CloudinaryService cloud, IOfferService offerService, IStayService stayService, IUserService userService)
        {
            _activityService = activityService;
            _bookingService = bookingService;
            _favoriteService = favoriteService;
            _travelService = travelService;
            this.userService = userService;
            _offerService = offerService;
            _stayService = stayService;
            _commentService = commentService;


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

        public async Task<IActionResult> Index(OfferFilterViewModel? filter)
        {
            var query = _offerService.GetAll().AsQueryable();
            var filterModel = new OfferFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {
                /*<th style="width: 50px">#</th>
                                <th>Name</th>
                                <th>Price</th>
                                <th>Rating</th>
                                <th>Comments</th>
                                <th>Actions</th>*/

                var model = _offerService.CombinedInclude().Include(x => x.User).Select(x => new OfferViewModel()
                {
                    OfferId = x.Id,
                    OfferName= x.Name,
                    OfferPic = x.CoverImage,
                    UserName = x.User.Username,
                    OfferPrice = x.Price,
                    Comments = x.Comments.ToList(),
                    //OfferRatingStars = x.R
                }).ToList();

                filterModel = new OfferFilterViewModel
                {
                    Offers = model,
                    Search = filter.Search,

                };
            }
            else 
            {
                var tempUsers = await userService.GetAllUserNamesAsync();
                var tempOffers = await _offerService.GetAllOfferNamesAsync();
                if (tempUsers.Contains(filter.Search))
                {
                    query = query.Where(x => x.User.Username == filter.Search);
                }
                if (tempOffers.Contains(filter.Search))
                {
                    query = query.Where(x => x.Name == filter.Search);
                }

                filterModel = new OfferFilterViewModel
                {
                    Offers = query.Include(x => x.User)
                .Select(x => new OfferViewModel()
                {
                    OfferId = x.Id,
                    OfferName = x.Name,
                    OfferPic = x.CoverImage,
                    UserName = x.User.Username,
                    OfferPrice = x.Price,
                    Comments = x.Comments.ToList(),

                }).ToList(),
                    Search = filter.Search
                };
            };

            return View(filterModel);
        }


        public async Task<IActionResult> AllOffer(OfferFilterViewModel? filter)
        {
            var query = _offerService.GetAll().AsQueryable();
            //var playlists = await playlistService.GetAllPlaylistsAsync();


            if (string.IsNullOrEmpty(filter.Search))
            {
                var model = _offerService.CombinedInclude().Include(x => x.User).ThenInclude(x => x.Offers).Select(x => new OfferViewModel()
                {
                    OfferId = x.Id,
                    OfferName = x.Name,
                    OfferPic = x.CoverImage,
                    UserName = x.User.Username,
                    OfferPrice = x.Price,
                    Comments = x.Comments.ToList(),
                    DefaultOfferRating = x.DefaultRating

                }).ToList();

                //

                foreach (var o in model)
                {
                    var tempOffer = await _offerService.GetOfferByIdAsync(o.OfferId);
                    var tempCom = await _commentService.GetAllCommentsAsync(x => x.OfferId == o.OfferId);
                    o.Comments = tempCom.ToList();
                    if (tempCom.Count() != 0)//ako ima reviewta
                    {
                        decimal rates = 0;
                        foreach (var r in tempCom)
                        {
                            rates += r.Stars;
                        }
                        decimal AverageRate;
                        decimal ofst;
                        if (tempOffer.Rating != null)//ako ofertata ima default rating
                        {
                            rates += tempOffer.Rating.Value;
                            int countt = tempCom.Count() + 1;
                            AverageRate = rates / countt;
                            ofst = Math.Round(AverageRate, 2);
                        }
                        else//ako nqma
                        {
                            AverageRate = rates / tempCom.Count();
                            ofst = Math.Round(AverageRate, 2);
                        }
                        o.OfferRating = AverageRate;
                        o.OfferRatingStars = ofst;
                    }
                    else//ako nqma reviewta
                    {
                        if (tempOffer.Rating != null)
                        {
                            o.OfferRatingStars = Math.Round(tempOffer.Rating.Value, 2);
                            o.OfferRating = tempOffer.Rating;
                        }
                        else
                        {
                            o.OfferRatingStars = 3;
                            o.OfferRating = 3;
                        }

                    }
                }
                //


                var filterModel = new OfferFilterViewModel
                {
                    Offers = model,

                };
                var sortedList = filterModel.Offers.OrderBy(x => x.OfferName).ToList();
                filterModel.Offers = sortedList;
                return View(filterModel);
            }
            else
            {

                if (!string.IsNullOrEmpty(filter.Search))
                {
                    query = query.Where(x => x.Name == filter.Search);
                }


                var filterModel = new OfferFilterViewModel
                {
                    Offers = query.Include(x => x.User).ThenInclude(x => x.Offers).Select(x => new OfferViewModel()
                    {
                        OfferId = x.Id,
                        OfferName = x.Name,
                        UserName = x.User.Username,
                        OfferPic = x.CoverImage
                    }).ToList(),
                    Search = filter.Search
                };
                return View(filterModel);
            }

        }
        public async Task<IActionResult> ListOffers()
        {
            IEnumerable<Offer> offers = await _offerService.GetAllOfferAsync();
            return View(offers);
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                
                await _offerService.DeleteOfferByIdAsync(id);
                await _commentService.DeleteAllCommentsByOffers(id);
                await _activityService.DeleteAllActivitysByOffers(id);
                await _travelService.DeleteAllTravelsByOffers(id);
                await _favoriteService.DeleteAllFavoritesByOffers(id);
                await _bookingService.DeleteAllBookingsByOffers(id);
            }
            return RedirectToAction("AllOffer");
        }

        public async Task<IActionResult> EditOffer(int Id)
        {

            var model = _offerService.GetAll()
                .Where(x => x.Id == Id)
                .Include(x => x.User)
                .Select(x => new EditOfferViewModel()
                {
                    Id = x.Id,
                    MaxPeople = x.MaxPeople,
                    Discount = x.Discount,
                    DurationDays = x.DurationDays,
                    Disc = x.Disc,
                    StartDate = x.StartDate,
                    LastDate = x.LastDate,
                    Rating = x.Rating,
                    Price = x.Price,
                    Name = x.Name,
                    StayId = x.StayId,
                    UserId = x.UserId,
                    CoverImage = x.CoverImage,
                    BackImage= x.BackImage
                })
                .FirstOrDefault();

            

            if (model == null)
            {
                return NotFound();
            }
            var stays = _stayService.GetAll();
            ViewBag.Stays = new SelectList(stays, "Id", "Name");

            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> EditOffer(EditOfferViewModel model)//ImageUrl
        {
            var tempS = await _stayService.GetStayByIdAsync(model.StayId.Value);
            if (model.Price <= tempS.Price)
            {
                TempData["error"] = "Цената на офертата трябва да е по-голяма от тази на престоя";
                var stays = _stayService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }
            if (model.Discount == null)
            {
                model.Discount = 0;
            }
            if (model.Discount < 0 || model.MaxPeople < 0 || model.DurationDays < 0 || model.Price < 0)
            {
                TempData["error"] = "Не използвайте негативни числа";
                var stays = _stayService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }
            if (model.LastDate.Value.Year <= model.StartDate.Value.Year)
            {
                if (model.LastDate <= model.StartDate)
                {
                    TempData["error"] = "Последната дата не може да е преди първата дата";
                    var stays = _stayService.GetAll();
                    ViewBag.Stays = new SelectList(stays, "Id", "Name");
                    return View(model);

                }
            }
            if (model.StartDate.Value.AddDays(model.DurationDays.Value) > model.LastDate)
            {
                TempData["error"] = "Продълши";
                var stays = _stayService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }

            var tempOffer = await _offerService.GetOfferAsync(x => x.Id == model.Id);
            if (tempOffer != null)
            {

                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                User user = await userService.GetUserAsync(x => x.Email == tempUser.Email);

                Offer track = _offerService.GetAll().FirstOrDefault(x => x.Id == model.Id);
                if (track == null)
                    return NotFound();


                if (model.Picture != null)
                {
                    var imageUploadResult = await cloudService.UploadImageAsync(model.Picture);
                    track.CoverImage = imageUploadResult;
                }
                if (model.BackPicture != null)
                {
                    var imageUploadResult = await cloudService.UploadImageAsync(model.BackPicture);
                    track.BackImage = imageUploadResult;
                }



                track.MaxPeople = model.MaxPeople;
                track.Discount = model.Discount;
                track.DurationDays = model.DurationDays;
                track.Disc = model.Disc;
                track.StartDate = model.StartDate;
                track.LastDate = model.LastDate;
                track.Rating = model.Rating;
                track.Price = model.Price;
                track.Name = model.Name;
                track.UserId = user.Id;
                track.StayId = model.StayId;



                await _offerService.UpdateOfferAsync(track);
                TempData["success"] = "Успешно редактиран запис";
            }
            else
            {
                TempData["error"] = "Това място за престой не съществува";
            }
            return RedirectToAction("AllOffer");

        }

        public async Task<IActionResult> AddOffer()
        {
            var stays = _stayService.GetAll();
            ViewBag.Stays = new SelectList(stays, "Id", "Name");
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddOffer(AddOfferViewModel model)
        {
            var tempS = await _stayService.GetStayByIdAsync(model.StayId.Value);
            if (model.Price <= tempS.Price)
            {
                TempData["error"] = "Цената на офертата трябва да е по-голяма от тази на престоя";
                var stays = _stayService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }
            if (model.Discount == null)
            {
                model.Discount = 0;
            }
            if (model.Discount < 0 || model.MaxPeople < 0 || model.DurationDays < 0 || model.Price < 0)
            {
                TempData["error"] = "Не използвайте негативни числа";
                var stays = _stayService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }
            if (model.LastDate.Value.Year <= model.StartDate.Value.Year)
            {
                if (model.LastDate <= model.StartDate)
                {
                    TempData["error"] = "Последната дата не може да е преди първата дата";
                    var stays = _stayService.GetAll();
                    ViewBag.Stays = new SelectList(stays, "Id", "Name");
                    return View(model);

                }
            }
            if (model.StartDate.Value.AddDays(model.DurationDays.Value) > model.LastDate)
            {
                TempData["error"] = "Продълши";
                var stays = _stayService.GetAll();
                ViewBag.Stays = new SelectList(stays, "Id", "Name");
                return View(model);
            }
            
            var tempStay = await _offerService.GetOfferAsync(x => x.Name == model.Name);
            if (tempStay == null)
            {
                var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
                User user = await userService.GetUserAsync(x => x.UserIdentity.Email == tempUser.Email);

                var imageUploadResult = await cloudService.UploadImageAsync(model.Picture);

                Console.WriteLine($"Found user in userService: {user.Email}");
                

                Offer offer = new Offer
                {
                    Name = model.Name,
                    MaxPeople = model.MaxPeople,
                    Discount = model.Discount,
                    Disc = model.Disc,
                    DurationDays = model.DurationDays,
                    StartDate = model.StartDate,
                    LastDate = model.LastDate,
                    Rating = model.Rating,
                    Price = model.Price,
                    UserId = user.Id,
                    StayId = model.StayId,
                    DefaultRating =model.Rating
                    
                };
                
                if (model.Picture != null && model.BackPicture != null)
                {
                    var imageUploadResult1 = await cloudService.UploadImageAsync(model.Picture);

                    var imageUploadResult2 = await cloudService.UploadImageAsync(model.BackPicture);
                    offer.CoverImage = imageUploadResult1;
                    offer.BackImage = imageUploadResult2;
                }
                if (model.Picture != null && model.BackPicture == null)
                {
                    var imageUploadResult1 = await cloudService.UploadImageAsync(model.Picture);
                    offer.CoverImage = imageUploadResult1;
                    offer.BackImage = "/Images/missing.jpg";
                }
                if (model.Picture == null && model.BackPicture != null)
                {
                    var imageUploadResult1 = await cloudService.UploadImageAsync(model.BackPicture);
                    offer.BackImage = imageUploadResult1;
                    offer.CoverImage = "/Images/missing.jpg";
                }
                if (model.Picture == null && model.BackPicture == null)
                {
                    offer.CoverImage = "/Images/missing.jpg";
                    offer.BackImage = "/Images/missing.jpg";
                }
                await _offerService.AddOfferAsync(offer);
                TempData["success"] = "Успешно записване";
                return RedirectToAction("AllOffer");

            }
            else
            {
                return View(model);
            }
        }

        
    }

}

