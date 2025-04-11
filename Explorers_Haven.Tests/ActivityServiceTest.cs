using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Explorers_Haven.Core.Services;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class ActivityServiceTests
    {
        private Mock<IRepository<Models.Activity>> _mockRepo;
        private ActivityService _activityService;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IRepository<Models.Activity>>();
            _activityService = new ActivityService(_mockRepo.Object);
        }

        [Test]
        public async Task AddActivityAsync_ShouldCallRepoAddAsync()
        {
            // Arrange
            var activity = new Models.Activity { Id = 1, Name = "Test Activity" };

            // Act
            await _activityService.AddActivityAsync(activity);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<Models.Activity>(a => a == activity)), Times.Once);
        }

        [Test]
        public async Task UpdateActivityAsync_ShouldCallRepoUpdateAsync()
        {
            // Arrange
            var activity = new Models.Activity { Id = 1, Name = "Updated Activity" };

            // Act
            await _activityService.UpdateActivityAsync(activity);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Models.Activity>(a => a == activity)), Times.Once);
        }

        [Test]
        public async Task DeleteActivityAsync_ShouldCallRepoDeleteAsync()
        {
            // Arrange
            var activity = new Models.Activity { Id = 1, Name = "Activity to Delete" };

            // Act
            await _activityService.DeleteActivityAsync(activity);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(It.Is<Models.Activity>(a => a == activity)), Times.Once);
        }

        [Test]
        public async Task DeleteActivityByIdAsync_ShouldCallRepoDeleteByIdAsync()
        {
            // Arrange
            var activityId = 1;

            // Act
            await _activityService.DeleteActivityByIdAsync(activityId);

            // Assert
            _mockRepo.Verify(r => r.DeleteByIdAsync(It.Is<int>(id => id == activityId)), Times.Once);
        }

        [Test]
        public async Task DeleteAllActivitysByOffers_ShouldDeleteAllActivitiesWithOfferId()
        {
            // Arrange
            var offerId = 1;
            var activities = new List<Models.Activity>
            {
                new Models.Activity { Id = 1, OfferId = offerId, Name = "Activity 1" },
                new Models.Activity { Id = 2, OfferId = offerId, Name = "Activity 2" }
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Models.Activity, bool>>>()))
                .ReturnsAsync(activities);

            // Act
            await _activityService.DeleteAllActivitysByOffers(offerId);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Models.Activity>()), Times.Exactly(2));
        }

        [Test]
        public void GetAll_ShouldReturnAllActivities()
        {
            // Arrange
            var activities = new List<Models.Activity>
            {
                new Models.Activity { Id = 1, Name = "Activity 1" },
                new Models.Activity { Id = 2, Name = "Activity 2" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAll()).Returns(activities);

            // Act
            var result = _activityService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetActivityByIdAsync_ShouldReturnActivity()
        {
            // Arrange
            var activityId = 1;
            var activity = new Models.Activity { Id = activityId, Name = "Test Activity" };
            _mockRepo.Setup(r => r.GetByIdAsync(activityId)).ReturnsAsync(activity);

            // Act
            var result = await _activityService.GetActivityByIdAsync(activityId);

            // Assert
            Assert.AreEqual(activity, result);
            _mockRepo.Verify(r => r.GetByIdAsync(It.Is<int>(id => id == activityId)), Times.Once);
        }

        [Test]
        public async Task GetAllActivityAsync_ShouldReturnAllActivitiesAsync()
        {
            // Arrange
            var activities = new List<Models.Activity>
            {
                new Models.Activity { Id = 1, Name = "Activity 1" },
                new Models.Activity { Id = 2, Name = "Activity 2" }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(activities);

            // Act
            var result = await _activityService.GetAllActivityAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAllActivityAsync_WithFilter_ShouldReturnFilteredActivities()
        {
            // Arrange
            var activities = new List<Models.Activity>
            {
                new Models.Activity { Id = 1, Name = "Activity 1" },
                new Models.Activity { Id = 2, Name = "Activity 2" }
            };

            _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Models.Activity, bool>>>()))
                .ReturnsAsync(activities.Where(a => a.Id == 1).ToList());

            // Act
            var result = await _activityService.GetAllActivityAsync(a => a.Id == 1);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [Test]
        public void CombinedInclude_ShouldIncludeProperties()
        {
            // Arrange
            var activities = new List<Models.Activity>
            {
                new Models.Activity { Id = 1, Name = "Activity 1" },
                new Models.Activity { Id = 2, Name = "Activity 2" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAllQuery()).Returns(activities);

            // Act
            var result = _activityService.CombinedInclude(x => x.Name);

            // Assert
            Assert.AreEqual(2, result.Count());
        }
    }
}
