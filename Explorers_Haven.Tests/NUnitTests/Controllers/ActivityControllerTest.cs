using Moq;
using NUnit.Framework;
using Explorers_Haven.Controllers;
using Explorers_Haven.Models;
using Explorers_Haven.ViewModels.Activity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Explorers_Haven.Core.IServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Explorers_Haven.Core.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class ActivityControllerTests
    {
        private Mock<IActivityService> _mockActivityService;
        private Mock<IOfferService> _mockOfferService;
        private Mock<IUserService> _mockUserService;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<CloudinaryService> _mockCloudService;
        private Mock<IConfiguration> _mockConfiguration;
        private ActivityController _controller;

        [SetUp]
        public void SetUp()
        {
            // Mocking the necessary services
            _mockActivityService = new Mock<IActivityService>();
            _mockOfferService = new Mock<IOfferService>();
            _mockUserService = new Mock<IUserService>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _mockCloudService = new Mock<CloudinaryService>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Ensure proper setup of mock dependencies
            _mockConfiguration.Setup(c => c["Cloudinary:CloudName"]).Returns("cloudName");
            _mockConfiguration.Setup(c => c["Cloudinary:ApiKey"]).Returns("apiKey");
            _mockConfiguration.Setup(c => c["Cloudinary:ApiSecret"]).Returns("apiSecret");

            // Create the controller and inject mocked services
            _controller = new ActivityController(
                _mockConfiguration.Object,
                _mockCloudService.Object,
                _mockUserService.Object,
                _mockUserManager.Object,
                _mockActivityService.Object,
                _mockOfferService.Object
            );

            // Initialize TempData to avoid null reference errors
            _controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        #region Index Tests


        [Test]
        public async Task Index_FilteredActivities_ReturnsCorrectView()
        {
            // Arrange
            var filter = new ActivityViewModel { Name = "Activity 1" };

            // Create a list of activities to be returned from the mock service
            var activities = new List<Activity>
            {
                new Activity { Id = 1, Name = "Activity 1" },
                new Activity { Id = 2, Name = "Activity 2" }
            };

            // Mock the method call to return the activities list
            _mockActivityService.Setup(s => s.GetAll()).Returns(activities.AsQueryable());

            // Act
            var result = await _controller.Index(filter) as ViewResult;
            var model = result?.Model as ActivityViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model?.Activities.Count);  // Only 1 activity should be filtered
            Assert.AreEqual("Activity 1", model?.Activities[0].Name);
        }

        #endregion

        #region ListActivities Tests

        [Test]
        public async Task ListActivities_ReturnsAllActivities()
        {
            // Arrange
            var activities = new List<Activity>
            {
                new Activity { Id = 1, Name = "Activity 1" },
                new Activity { Id = 2, Name = "Activity 2" }
            };

            // Mock the method call to return all activities
            _mockActivityService.Setup(s => s.GetAllActivityAsync()).ReturnsAsync(activities);

            // Act
            var result = await _controller.ListActivities() as ViewResult;
            var model = result?.Model as IEnumerable<Activity>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, model?.Count()); // Should return all activities
        }

        #endregion

        #region Delete Tests

        [Test]
        public async Task Delete_ActivityIdExists_DeletesActivity()
        {
            // Arrange
            int activityId = 1;

            // Mock Delete call
            _mockActivityService.Setup(s => s.DeleteActivityByIdAsync(activityId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(activityId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AllActivity", result?.ActionName); // Should redirect to AllActivity
            Assert.AreEqual("success", _controller.TempData["success"]);
        }

        #endregion

        #region AllActivity Tests

        [Test]
        public async Task AllActivity_FilteredByTitle_ReturnsFilteredActivities()
        {
            // Arrange
            var filter = new ActivityFilterViewModel { Title = "Activity 1" };

            var activities = new List<Activity>
            {
                new Activity { Id = 1, Name = "Activity 1", Offer = new Offer { Name = "Offer 1" } },
                new Activity { Id = 2, Name = "Activity 2", Offer = new Offer { Name = "Offer 2" } }
            };

            _mockActivityService.Setup(s => s.GetAll()).Returns(activities.AsQueryable());

            // Act
            var result = await _controller.AllActivity(filter) as ViewResult;
            var model = result?.Model as ActivityFilterViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model?.Activities.Count); // Should only return 1 activity matching the title
        }

        #endregion

        #region EditActivity Tests (GET and POST)

        [Test]
        public async Task EditActivity_Get_ReturnsEditView()
        {
            // Arrange
            int activityId = 1;
            var activity = new Activity { Id = activityId, Name = "Activity 1" };

            _mockActivityService.Setup(s => s.GetAll()).Returns(new List<Activity> { activity }.AsQueryable());
            _mockOfferService.Setup(s => s.GetAllOfferAsync()).ReturnsAsync(new List<Offer> { new Offer { Id = 1, Name = "Offer 1" } });

            // Act
            var result = await _controller.EditActivity(activityId) as ViewResult;
            var model = result?.Model as EditActivityViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(activityId, model?.Id);
        }

        [Test]
        public async Task EditActivity_Post_UpdatesActivity()
        {
            // Arrange
            var model = new EditActivityViewModel { Id = 1, Name = "Updated Activity", OfferId = 1 };
            var existingActivity = new Activity { Id = 1, Name = "Old Activity", OfferId = 1 };

            _mockActivityService.Setup(s => s.GetActivityAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Activity, bool>>>())).ReturnsAsync(existingActivity);
            _mockActivityService.Setup(s => s.UpdateActivityAsync(It.IsAny<Activity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.EditActivity(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AllActivity", result?.ActionName);
            Assert.AreEqual("success", _controller.TempData["success"]);
        }

        #endregion

        #region AddActivity Tests (GET and POST)

        [Test]
        public async Task AddActivity_Get_ReturnsAddActivityView()
        {
            // Arrange
            _mockOfferService.Setup(s => s.GetAll()).Returns(new List<Offer> { new Offer { Id = 1, Name = "Offer 1" } });

            // Act
            var result = await _controller.AddActivity() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddActivity_Post_AddsNewActivity()
        {
            // Arrange
            var model = new AddActivityViewModel { Name = "New Activity", OfferId = 1 };
            var user = new User { Id = "user1", Username = "testUser" };

            _mockUserService.Setup(s => s.GetUserAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<User, bool>>>())).ReturnsAsync(user);
            _mockActivityService.Setup(s => s.AddActivityAsync(It.IsAny<Activity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddActivity(model) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("AllActivity", result?.ActionName);
            Assert.AreEqual("success", _controller.TempData["success"]);
        }

        #endregion
    }
}
