﻿using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.Controllers
{
    public class CommentController : Controller
    {
        ICommentService _commentService;
        IOfferService _offerService;
        private readonly UserManager<IdentityUser> _userManager;
        IUserService _userService;


        public CommentController(IUserService userService, UserManager<IdentityUser> userManager, IOfferService offerService, ICommentService CommentService)
        {
            _commentService = CommentService;
            _offerService = offerService;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IActionResult> DeleteComment(int id)
        {
            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Comment b = await _commentService.GetCommentAsync(x => x.OfferId == id && x.UserId == user.Id);
            if (b != null)
            {
                await _commentService.DeleteCommentAsync(b); TempData["success"] = "Comment canceled!";
                return RedirectToAction("HomePage", "Home");
            }
            TempData["error"] = "Comment doesn't exist!";
            return RedirectToAction("OfferPage", "Home");
        }
        public async Task<IActionResult> WriteComment(int id, string comment, int rating)
        {
            if (string.IsNullOrEmpty(comment))
            {
                return Json(new { success = false, message = "Can't add empty comments!" });
            }

            var tempUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            User user = await _userService.GetUserAsync(x => x.Email == tempUser.Email);

            Offer offer = await _offerService.GetOfferByIdAsync(id);
            var com = await _commentService.GetCommentAsync(x => x.UserId == user.Id && x.OfferId == offer.Id);
            if (com == null)
            {
                Comment newComment = new Comment()
                {
                    OfferId = id,
                    Offer = offer,
                    Stars = rating,
                    Content = comment,
                    User = user,
                    UserId = user.Id
                };

                await _commentService.AddCommentAsync(newComment);
                return RedirectToAction("OfferPage", "Home", new { Id = id });
            }
            else
            {
                com.Content = comment;
                com.Stars = rating;
                await _commentService.UpdateCommentAsync(com);
                return RedirectToAction("OfferPage", "Home", new { Id = id });
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
