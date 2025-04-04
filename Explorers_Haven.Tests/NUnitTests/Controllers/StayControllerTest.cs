using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Explorers_Haven.Controllers;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Tests.Controllers
{
    [TestFixture]
    public class StayControllerTests
    {
        private Mock<IStayService> _mockStayService;
        private Mock<IOfferService> _mockOfferService;
        private Mock<IUserService> _mockUserService;
        private Mock<IStayAmenityService> _mockStayAmenityService;
        private Mock<IAmenityService> _mockAmenityService;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<CloudinaryService> _mockCloudService;
        private StayController _controller;

        [SetUp]
        public void SetUp()
        {
            // Create mocks for all dependencies
            _mockStayService = new Mock<IStayService>();
            _mockOfferService = new Mock<IOfferService>();
            _mockUserService = new Mock<IUserService>();
            _mockStayAmenityService = new Mock<IStayAmenityService>();
            _mockAmenityService = new Mock<IAmenityService>();

            var mockUserManager = new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object, null, null, null, null, null, null, null, null);

            _mockUserManager = mockUserManager;
            _mockCloudService = new Mock<CloudinaryService>();

            // Initialize controller with mocked services
            _controller = new StayController(
                _mockStayAmenityService.Object,
                _mockAmenityService.Object,
                _mockUserManager.Object,
                new Mock<IConfiguration>().Object,
                _mockCloudService.Object,
                _mockStayService.Object,
                _mockOfferService.Object,
                _mockUserService.Object
            );
        }

        [Test]
        public async Task Index_ShouldReturnViewWithFilteredStays_WhenFilterIsApplied()
        {
            // Arrange
            var filter = new StayViewModel { Name = "Test Stay" };
            var stays = new List<Stay>
            {
                new Stay { Id = 1, Name = "Test Stay", Price = 100, Stars = 4, Image = "test.jpg" }
            };
            _mockStayService.Setup(s => s.GetAll()).Returns(stays.AsQueryable());

            // Act
            var result = await _controller.Index(filter);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as StayViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Stays.Count);
            Assert.AreEqual("Test Stay", model.Stays[0].Name);
        }

        [Test]
        public async Task Delete_ShouldRemoveStay_WhenStayExists()
        {
            // Arrange
            var stayId = 1;
            _mockStayService.Setup(s => s.DeleteStayByIdAsync(It.IsAny<int>())).Returns(Task.CompletedTask);
            _mockOfferService.Setup(o => o.DeleteAllOffersByStays(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(stayId);

            // Assert
            _mockStayService.Verify(s => s.DeleteStayByIdAsync(It.Is<int>(id => id == stayId)), Times.Once);
            _mockOfferService.Verify(o => o.DeleteAllOffersByStays(It.Is<int>(id => id == stayId)), Times.Once);
            var tempData = _controller.TempData["success"];
            Assert.AreEqual("Успешно изтрит запис", tempData);
        }

        [Test]
        public async Task EditStay_ShouldUpdateStay_WhenValidDataIsSubmitted()
        {
            // Arrange
            var model = new EditStayViewModel
            {
                Id = 1,
                Title = "Updated Stay",
                Price = 150,
                Stars = 5,
                Disc = "Updated description",
                SelectedAmenities = new int[] { 1, 2 }
            };

            var existingStay = new Stay
            {
                Id = 1,
                Name = "Old Stay",
                Price = 100,
                Stars = 4,
                Disc = "Old description",
                UserId = 100
            };

            _mockStayService.Setup(s => s.GetStayAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Stay, bool>>>())).ReturnsAsync(existingStay);
            _mockStayService.Setup(s => s.UpdateStayAsync(It.IsAny<Stay>())).Returns(Task.CompletedTask);
           // _mockUserService.Setup(u => u.GetUserAsync(It.IsAny<System.Func<User, bool>>())).ReturnsAsync(new User { Id = 100, Username = "testUser" });
            _mockStayAmenityService.Setup(sa => sa.AddStayAmenityAsync(It.IsAny<StayAmenity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.EditStay(model);

            // Assert
            var viewResult = result as RedirectToActionResult;
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("AllStay", viewResult.ActionName);
            _mockStayService.Verify(s => s.UpdateStayAsync(It.IsAny<Stay>()), Times.Once);
        }

        [Test]
        public async Task AddStay_ShouldReturnErrorMessage_WhenStayAlreadyExists()
        {
            // Arrange
            var model = new AddStayViewModel
            {
                Title = "Existing Stay",
                Disc = "Description",
                Price = "200",
                Stars = 5
            };

            _mockStayService.Setup(s => s.GetStayAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Stay, bool>>>())).ReturnsAsync(new Stay());

            // Act
            var result = await _controller.AddStay(model);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("AllStay", redirectResult.ActionName);
            var errorMessage = _controller.TempData["error"];
            Assert.AreEqual("Това място за престой вече съществува", errorMessage);
        }
    }
}
