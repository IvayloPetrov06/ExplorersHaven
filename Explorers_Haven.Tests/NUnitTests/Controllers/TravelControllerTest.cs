using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Moq;
using Explorers_Haven.Controllers;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Travel;
using Explorers_Haven.ViewModels.Offer;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class TravelControllerTests
    {
        private Mock<ITravelService> _mockTravelService;
        private Mock<IUserService> _mockUserService;
        private Mock<ITransportService> _mockTransportService;
        private Mock<IOfferService> _mockOfferService;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private TravelController _controller;

        [SetUp]
        public void Setup()
        {
            _mockTravelService = new Mock<ITravelService>();
            _mockUserService = new Mock<IUserService>();
            _mockTransportService = new Mock<ITransportService>();
            _mockOfferService = new Mock<IOfferService>();

            var mockUserStore = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            _controller = new TravelController(
                _mockUserService.Object,
                _mockUserManager.Object,
                _mockTransportService.Object,
                _mockTravelService.Object,
                _mockOfferService.Object
            );
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        }
        [TearDown]
        public void TearDown()
        {
            // Dispose of the controller to clean up resources
            _controller?.Dispose();
        }
        #region AddTravel Tests

        [Test]
        public async Task AddTravel_ShouldReturnRedirectToActionResult_WhenValidModel()
        {
            // Arrange
            var model = new AddTravelViewModel
            {
                Start = "Start Location",
                Finish = "End Location",
                DurationDays = 5,
                Arrival = true,
                TransportId = 1,
                OfferId = 1
            };

            // Mock services
            _mockTravelService.Setup(s => s.AddTravelAsync(It.IsAny<Travel>())).Returns(Task.CompletedTask);
            _mockUserService.Setup(s => s.GetUserAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<User, bool>>>())).ReturnsAsync(new User { Id = 1 });

            // Act
            var result = await _controller.AddTravel(model);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("AllTravel", redirectResult.ActionName);
        }

        [Test]
        public async Task AddTravel_ShouldReturnViewWithError_WhenArrivalConflict()
        {
            // Arrange
            var model = new AddTravelViewModel
            {
                Start = "Start Location",
                Finish = "End Location",
                DurationDays = 5,
                Arrival = true,
                TransportId = 1,
                OfferId = 1
            };

            // Mock services for conflicting arrival
            _mockTravelService.Setup(s => s.GetAllTravelAsync()).ReturnsAsync(new List<Travel>
    {
        new Travel { OfferId = 1, Arrival = true }
    });

            // Act
            var result = await _controller.AddTravel(model);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);

            // Now check TempData for the error message
            var errorMessage = _controller.TempData["error"];
            Assert.IsNotNull(errorMessage);
            Assert.AreEqual("Офертата вече има отиване", errorMessage);
        }

        #endregion

        #region EditTravel Tests

        [Test]
        public async Task EditTravel_ShouldReturnViewResult_WhenValidId()
        {
            // Arrange
            var model = new EditTravelViewModel
            {
                Id = 1,
                Start = "Start Location",
                Finish = "End Location",
                DurationDays = 5,
                Arrival = true,
                TransportId = 1,
                OfferId = 1,
                UserId = 1
            };

            //_mockTravelService.Setup(s => s.GetAll())
                //.Returns(new List<Travel> { new Travel { Id = 1, Start = "Start Location", Finish = "End Location", TransportId = 1, OfferId = 1 } });

            _mockUserService.Setup(s => s.GetUserAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<User, bool>>>())).ReturnsAsync(new User { Id = 1 });

            // Act
            var result = await _controller.EditTravel(1);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.IsNotNull(viewResult.Model);
        }

        [Test]
        public async Task EditTravel_ShouldReturnRedirectToAction_WhenValidUpdate()
        {
            // Arrange
            var model = new EditTravelViewModel
            {
                Id = 1,
                Start = "Updated Start Location",
                Finish = "Updated End Location",
                DurationDays = 7,
                Arrival = false,
                TransportId = 1,
                OfferId = 1,
                UserId = 1
            };

            //_mockTravelService.Setup(s => s.GetAll()).Returns(new List<Travel> { new Travel { Id = 1, OfferId = 1, Arrival = true } });
            _mockTravelService.Setup(s => s.UpdateTravelAsync(It.IsAny<Travel>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.EditTravel(model);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("AllTravel", redirectResult.ActionName);
        }

        #endregion

        #region Delete Tests

        [Test]
        public async Task Delete_ShouldReturnRedirectToAction_WhenValidId()
        {
            // Arrange
            var travelId = 1;

            // Mock service
            _mockTravelService.Setup(s => s.DeleteTravelByIdAsync(travelId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(travelId);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("AllTravel", redirectResult.ActionName);
        }

        #endregion

        #region AllTravel Tests

        [Test]
        public async Task AllTravel_ShouldReturnViewWithTravels_WhenNoFilter()
        {
            // Arrange
            var model = new TravelFilterViewModel
            {
                Title = null
            };

            _mockTravelService.Setup(s => s.CombinedInclude())
                .Returns(new List<Travel>
                {
                    new Travel { Id = 1, Start = "Start Location", Finish = "End Location", Offer = new Offer { Name = "Offer 1" }, User = new User { Username = "User 1" }, Transport = new Transport { Name = "Transport 1" }, Arrival = true }
                }.AsQueryable());

            // Act
            var result = await _controller.AllTravel(model);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            var modelResult = viewResult.Model as TravelFilterViewModel;
            Assert.IsNotNull(modelResult);
            Assert.AreEqual(1, modelResult.Travels.Count);
        }

        #endregion
    }
}
