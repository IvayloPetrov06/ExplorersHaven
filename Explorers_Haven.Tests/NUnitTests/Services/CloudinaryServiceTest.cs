using Moq;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Explorers_Haven.Core.Services;
using System.Collections.Generic;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class CloudinaryServiceTests
    {
        private Mock<IConfiguration> _mockConfig;
        private Mock<Cloudinary> _mockCloudinary;
        private CloudinaryService _cloudinaryService;

        [SetUp]
        public void Setup()
        {
            // Mock IConfiguration
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(config => config["Cloudinary:CloudName"]).Returns("TestCloudName");
            _mockConfig.Setup(config => config["Cloudinary:ApiKey"]).Returns("TestApiKey");
            _mockConfig.Setup(config => config["Cloudinary:ApiSecret"]).Returns("TestApiSecret");

            // Initialize the CloudinaryService with mocked configuration
            _cloudinaryService = new CloudinaryService(_mockConfig.Object);

            // Mock Cloudinary object (this allows us to mock UploadAsync)
            _mockCloudinary = new Mock<Cloudinary>(new Account("TestCloudName", "TestApiKey", "TestApiSecret"));
        }

        #region Upload Image

        [Test]
        public async Task UploadImageAsync_ShouldReturnUrl_WhenUploadSuccessful()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(new byte[0]));
            mockFile.Setup(f => f.FileName).Returns("image.jpg");

            var uploadResult = new ImageUploadResult
            {
                SecureUrl = new Uri("https://cloudinary.com/test_image.jpg")
            };

            //_mockCloudinary.Setup(c => c.UploadAsync(It.IsAny<ImageUploadParams>())).ReturnsAsync(uploadResult);

            // Act
            var result = await _cloudinaryService.UploadImageAsync(mockFile.Object);

            // Assert
            Assert.AreEqual("https://cloudinary.com/test_image.jpg", result);
        }

        [Test]
        public async Task UploadImageAsync_ShouldReturnNull_WhenFileIsNull()
        {
            // Arrange
            IFormFile mockFile = null;

            // Act
            var result = await _cloudinaryService.UploadImageAsync(mockFile);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UploadImageAsync_ShouldReturnNull_WhenFileIsEmpty()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.Length).Returns(0);

            // Act
            var result = await _cloudinaryService.UploadImageAsync(mockFile.Object);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UploadImageAsync_ShouldReturnNull_WhenUploadFails()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(new byte[0]));
            mockFile.Setup(f => f.FileName).Returns("image.jpg");

            var uploadResult = new ImageUploadResult
            {
                SecureUrl = null
            };

            //_mockCloudinary.Setup(c => c.UploadAsync(It.IsAny<ImageUploadParams>())).ReturnsAsync(uploadResult);

            // Act
            var result = await _cloudinaryService.UploadImageAsync(mockFile.Object);

            // Assert
            Assert.IsNull(result);
        }

        #endregion
    }
}
