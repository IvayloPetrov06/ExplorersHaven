using CloudinaryDotNet;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Explorers_Haven.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Explorers_Haven.Controllers
{
    public class UserController : Controller
    {
        IUserService userService;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _configuration;
        CloudinaryService cloudService;

        public UserController(IConfiguration configuration, CloudinaryService cloud, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, IUserService _userService)
        {
            userManager = _userManager;
            userService = _userService;
            signInManager = _signInManager;

            this.cloudService = cloud;

            _configuration = configuration;
            var account = new Account(
           _configuration["Cloudinary:CloudName"],
           _configuration["Cloudinary:ApiKey"],
           _configuration["Cloudinary:ApiSecret"]
       );
            _cloudinary = new Cloudinary(account);
        }
        public async Task<IActionResult> AllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return View(users);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                await userService.DeleteUserByIdAsync(id);
                return RedirectToAction("AllUsers");
            }
            else
            {
                return RedirectToAction("AllUsers");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserViewModel model)
        {
            User user = await userService.GetUserAsync(x => x.Email == model.Email);
            user.Username = model.Name;
            user.ProfilePicture = model.ProfilePicture;
            user.Bio = model.Bio;
            user.Email = model.Email;
            await userService.UpdateUserAsync(user);

            return View(model);
        }
        public async Task<IActionResult> Profile()
        {
            var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
            if (tempUser == null)
            {
                return NotFound("No Identity user found.");
            }
            User user = await userService.GetUserAsync(x => x.Email == tempUser.Email);
            if (user == null)
            {
                return NotFound("No application user found for email: " + tempUser.Email);
            }
            UserViewModel model = new UserViewModel
            {
                Email = user.Email,
                Bio = user.Bio,
                Name = user.Username,
                ProfilePicture = user.ProfilePicture
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UserViewModel user)
        {
            var tempUser = await userManager.FindByEmailAsync(User.Identity.Name);
            User userModel = await userService.GetUserAsync(x => x.Email == tempUser.Email);

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (userModel == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            userModel.Username = user.Name;
            userModel.Email = user.Email;
            userModel.Bio = user.Bio;
            userModel.ProfilePicture = user.ProfilePicture;

            if (user.ImageFile != null)
            {
                var imageUploadResult = await cloudService.UploadImageAsync(user.ImageFile);
                userModel.ProfilePicture = imageUploadResult;
            }

            await userService.UpdateUserAsync(userModel);
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> Update(int id)
        {
            User user = await userService.GetUserByIdAsync(id);
            UserViewModel model = new UserViewModel
            {
                Email = user.Email,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture,
                Name = user.Username,
            };
            return View(model);
        }
        public IActionResult AddUser()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            //if (user.ProfilePicture == null)
            //{
            //    user.ProfilePicture = "/Images/def.jpg";
            //}
            if (ModelState.IsValid)
            {
                await userService.AddUserAsync(user);
                return RedirectToAction("AllUsers");
            }
            else
            {
                return View(user);
            }
        }
    }
}
