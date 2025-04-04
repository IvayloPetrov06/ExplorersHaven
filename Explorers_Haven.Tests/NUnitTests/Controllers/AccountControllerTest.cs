using Moq;
using NUnit.Framework;
using Explorers_Haven.Controllers;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;
using Explorers_Haven.Core.IServices;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<SignInManager<IdentityUser>> _mockSignInManager;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private Mock<IService<User>> _mockUserService;
        private AccountController _controller;

        [SetUp]
        public void SetUp()
        {
            // Mock the dependencies
            _mockUserManager = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _mockSignInManager = new Mock<SignInManager<IdentityUser>>(_mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(), null, null, null);
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
            _mockUserService = new Mock<IService<User>>();

            // Initialize the controller with the mocked dependencies
            _controller = new AccountController(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockRoleManager.Object,
                _mockUserService.Object
            );

            // Initialize TempData with a mock to avoid null reference error
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        }
        [TearDown]
        public void TearDown()
        {
            // Dispose of the controller to clean up resources
            _controller?.Dispose();
        }

        #region Login Tests

        [Test]
        public async Task Login_Post_InvalidEmail_ShouldReturnError()
        {
            // Arrange
            var model = new LoginViewModel { Email = "invalid@domain.com", Password = "password" };

            // Mock UserManager to return null for the given email
            _mockUserManager.Setup(m => m.FindByEmailAsync(model.Email)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("User not found.", _controller.TempData["Error"]);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Login_Post_InvalidPassword_ShouldReturnError()
        {
            // Arrange
            var model = new LoginViewModel { Email = "test@domain.com", Password = "wrongpassword" };
            var user = new IdentityUser { UserName = "test@domain.com", Email = "test@domain.com" };

            // Mock UserManager to return the user and SignInManager to return failed sign-in
            _mockUserManager.Setup(m => m.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockSignInManager.Setup(m => m.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var viewResult = result as ViewResult;
            Assert.AreEqual("Invalid login attempt.", _controller.TempData["Error"]);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Login_Post_SuccessfulLogin_ShouldRedirect()
        {
            // Arrange
            var model = new LoginViewModel { Email = "test@domain.com", Password = "password", RememberMe = false };
            var user = new IdentityUser { UserName = "test@domain.com", Email = "test@domain.com" };

            // Mock UserManager to return the user and SignInManager to return success
            _mockUserManager.Setup(m => m.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            _mockSignInManager.Setup(m => m.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("HomePage", redirectResult.ActionName);
            Assert.AreEqual("Home", redirectResult.ControllerName);
        }

        #endregion
    }
}

