using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Explorers_Haven.ViewModels.Account;

namespace Explorers_Haven.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IService<User> _userService;
        public AccountController(
            UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IService<User> userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;

        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData["Error"] = "User not found.";
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false);         

                if (result.Succeeded)
                {
                    TempData["Success"] = "Влизането е успешно";
                    return RedirectToAction("HomePage", "Home");
                }
                TempData["Error"] = "Invalid login attempt.";
                ModelState.AddModelError("", "Invalid login attempt.");

            }
            return View(model);
        }

        public IActionResult Register() => View();


        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                                   await _signInManager.SignInAsync(user, isPersistent: false);
                    User user2 = new User()
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Password = model.Password,
                        UserIdentity = user,
                        UserIdentityId = user.Id
                    };
                    if (user2.ProfilePicture == null)
                    {
                        user2.ProfilePicture = "/Images/def.jpg";
                    }
                    await _userService.AddAsync(user2);
                    return RedirectToAction("HomePage", "Home");

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Register", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
 

