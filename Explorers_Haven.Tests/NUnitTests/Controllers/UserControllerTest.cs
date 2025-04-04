using Moq;
using NUnit.Framework;
using Explorers_Haven.Controllers;
using Explorers_Haven.Models;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using Explorers_Haven.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserService> mockUserService;
        private Mock<UserManager<IdentityUser>> mockUserManager;
        private Mock<SignInManager<IdentityUser>> mockSignInManager;
        private Mock<CloudinaryService> mockCloudinaryService;
        private Mock<IConfiguration> mockConfiguration;
        private UserController controller;

        [SetUp]
        public void SetUp()
        {
            mockUserService = new Mock<IUserService>();
            mockUserManager = new Mock<UserManager<IdentityUser>>();
            mockSignInManager = new Mock<SignInManager<IdentityUser>>();
            mockCloudinaryService = new Mock<CloudinaryService>();
            mockConfiguration = new Mock<IConfiguration>();

            controller = new UserController(
                mockConfiguration.Object,
                mockCloudinaryService.Object,
                mockUserManager.Object,
                mockSignInManager.Object,
                mockUserService.Object
            );
            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        }
        [TearDown]
        public void TearDown()
        {
            // Dispose of the controller to clean up resources
            controller?.Dispose();
        }

        #region AllUsers

        [Test]
        public async Task AllUsers_ReturnsViewWithUsers()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Username = "TestUser", Email = "test@example.com" } };
            mockUserService.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await controller.AllUsers();

            // Assert
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
            Assert.AreEqual(users, viewResult.Model);
        }

        #endregion

        #region Delete

        [Test]
        public async Task Delete_RemovesUserAndRedirects()
        {
            // Arrange
            int userId = 1;
            mockUserService.Setup(x => x.DeleteUserByIdAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.Delete(userId);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);
            Assert.AreEqual("AllUsers", redirectResult.ActionName);
            mockUserService.Verify(x => x.DeleteUserByIdAsync(userId), Times.Once);
        }

        [Test]
        public async Task Delete_UserIdIsNull_RedirectsToAllUsers()
        {
            // Act
            var result = await controller.Delete(null);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);
            Assert.AreEqual("AllUsers", redirectResult.ActionName);
        }

        #endregion

        #region Profile (GET)

        [Test]
        public async Task Profile_ReturnsViewWithUserInfo()
        {
            // Arrange
            var identityUser = new IdentityUser { UserName = "test@example.com" };
            var user = new User { Email = "test@example.com", Username = "TestUser", Bio = "Bio", ProfilePicture = "profile.jpg" };
            var userViewModel = new UserViewModel { Email = user.Email, Bio = user.Bio, Name = user.Username, ProfilePicture = user.ProfilePicture };

            mockUserManager.Setup(x => x.FindByEmailAsync(identityUser.UserName)).ReturnsAsync(identityUser);
            //mockUserService.Setup(x => x.GetUserAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync(user);

            // Act
            var result = await controller.Profile();

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult?.Model as UserViewModel;
            Assert.NotNull(viewResult);
            Assert.AreEqual(userViewModel.Email, model?.Email);
            Assert.AreEqual(userViewModel.Name, model?.Name);
            Assert.AreEqual(userViewModel.Bio, model?.Bio);
            Assert.AreEqual(userViewModel.ProfilePicture, model?.ProfilePicture);
        }

        #endregion

        #region Profile (POST)

        [Test]
        public async Task Profile_PostUpdatesUserInfo()
        {
            // Arrange
            var model = new UserViewModel
            {
                Email = "test@example.com",
                Name = "UpdatedUser",
                Bio = "Updated Bio",
                ProfilePicture = "newProfilePic.jpg"
            };

            var user = new User
            {
                Email = "test@example.com",
                Username = "TestUser",
                ProfilePicture = "profile.jpg",
                Bio = "Bio"
            };

            //mockUserService.Setup(x => x.GetUserAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync(user);
            mockUserService.Setup(x => x.UpdateUserAsync(user)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.Profile(model);

            // Assert
            var viewResult = result as ViewResult;
            mockUserService.Verify(x => x.UpdateUserAsync(user), Times.Once);
            Assert.NotNull(viewResult);
        }

        #endregion

        #region AddUser (POST)

        [Test]
        public async Task AddUser_PostValidUser_CreatesUserAndRedirects()
        {
            // Arrange
            var user = new User
            {
                Username = "NewUser",
                Email = "newuser@example.com",
                ProfilePicture = "default.jpg",
                Bio = "New User Bio"
            };

            mockUserService.Setup(x => x.AddUserAsync(user)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.AddUser(user);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);
            Assert.AreEqual("AllUsers", redirectResult.ActionName);
            mockUserService.Verify(x => x.AddUserAsync(user), Times.Once);
        }

        [Test]
        public async Task AddUser_PostInvalidUser_ReturnsView()
        {
            // Arrange
            var user = new User { Email = "invaliduser@example.com" };
            controller.ModelState.AddModelError("Error", "Model is invalid");

            // Act
            var result = await controller.AddUser(user);

            // Assert
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);
        }

        #endregion

        #region Update (POST)

        [Test]
        public async Task Update_PostValidUser_UpdatesUser()
        {
            // Arrange
            var userViewModel = new UserViewModel
            {
                Name = "Updated User",
                Email = "updated@example.com",
                Bio = "Updated bio",
                ProfilePicture = "updatedPic.jpg"
            };

            var user = new User { Email = "updated@example.com", Username = "OldUser" };

            mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new IdentityUser());
            //mockUserService.Setup(x => x.GetUserAsync(It.IsAny<Func<User, bool>>())).ReturnsAsync(user);
            mockUserService.Setup(x => x.UpdateUserAsync(user)).Returns(Task.CompletedTask);

            // Act
            var result = await controller.Update(1, userViewModel);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.NotNull(redirectResult);
            Assert.AreEqual("Profile", redirectResult.ActionName);
            mockUserService.Verify(x => x.UpdateUserAsync(user), Times.Once);
        }

        #endregion
    }
}
