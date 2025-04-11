using Moq;
using NUnit.Framework;
using Explorers_Haven.Core.Services;
using Explorers_Haven.Models;
using Explorers_Haven.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private Mock<IRepository<Comment>> _mockCommentRepository;
        private CommentService _commentService;

        [SetUp]
        public void SetUp()
        {
            // Mock the repository
            _mockCommentRepository = new Mock<IRepository<Comment>>();

            // Initialize the service with the mocked repository
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        #region AddCommentAsync

        [Test]
        public async Task AddCommentAsync_ShouldAddComment()
        {
            // Arrange
            var comment = new Comment { Id = 1, Content = "Test Comment", OfferId = 1 };

            _mockCommentRepository.Setup(r => r.AddAsync(comment)).Returns(Task.CompletedTask);

            // Act
            await _commentService.AddCommentAsync(comment);

            // Assert
            _mockCommentRepository.Verify(r => r.AddAsync(comment), Times.Once);
        }

        #endregion

        #region DeleteCommentAsync

        [Test]
        public async Task DeleteCommentAsync_ShouldDeleteComment()
        {
            // Arrange
            var comment = new Comment { Id = 1, Content = "Test Comment", OfferId = 1 };

            _mockCommentRepository.Setup(r => r.DeleteAsync(comment)).Returns(Task.CompletedTask);

            // Act
            await _commentService.DeleteCommentAsync(comment);

            // Assert
            _mockCommentRepository.Verify(r => r.DeleteAsync(comment), Times.Once);
        }

        #endregion

        #region DeleteCommentByIdAsync

        [Test]
        public async Task DeleteCommentByIdAsync_ShouldDeleteCommentById()
        {
            // Arrange
            int commentId = 1;
            _mockCommentRepository.Setup(r => r.DeleteByIdAsync(commentId)).Returns(Task.CompletedTask);

            // Act
            await _commentService.DeleteCommentByIdAsync(commentId);

            // Assert
            _mockCommentRepository.Verify(r => r.DeleteByIdAsync(commentId), Times.Once);
        }

        #endregion

        #region DeleteAllCommentsByOffers

        [Test]
        public async Task DeleteAllCommentsByOffers_ShouldDeleteAllCommentsWithOfferId()
        {
            // Arrange
            int offerId = 1;
            var comments = new List<Comment>
            {
                new Comment { Id = 1, Content = "Test Comment 1", OfferId = offerId },
                new Comment { Id = 2, Content = "Test Comment 2", OfferId = offerId }
            };

            _mockCommentRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(comments);
            _mockCommentRepository.Setup(r => r.DeleteAsync(It.IsAny<Comment>())).Returns(Task.CompletedTask);

            // Act
            await _commentService.DeleteAllCommentsByOffers(offerId);

            // Assert
            _mockCommentRepository.Verify(r => r.DeleteAsync(It.IsAny<Comment>()), Times.Exactly(2));
        }

        #endregion

        #region GetAll

        [Test]
        public void GetAll_ShouldReturnAllComments()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { Id = 1, Content = "Test Comment 1", OfferId = 1 },
                new Comment { Id = 2, Content = "Test Comment 2", OfferId = 1 }
            }.AsQueryable();

            _mockCommentRepository.Setup(r => r.GetAll()).Returns(comments);

            // Act
            var result = _commentService.GetAll();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetCommentByIdAsync

        [Test]
        public async Task GetCommentByIdAsync_ShouldReturnComment()
        {
            // Arrange
            int commentId = 1;
            var comment = new Comment { Id = commentId, Content = "Test Comment", OfferId = 1 };
            _mockCommentRepository.Setup(r => r.GetByIdAsync(commentId)).ReturnsAsync(comment);

            // Act
            var result = await _commentService.GetCommentByIdAsync(commentId);

            // Assert
            Assert.AreEqual(comment, result);
        }

        #endregion

        #region UpdateCommentAsync

        [Test]
        public async Task UpdateCommentAsync_ShouldUpdateComment()
        {
            // Arrange
            var comment = new Comment { Id = 1, Content = "Updated Comment", OfferId = 1 };
            _mockCommentRepository.Setup(r => r.UpdateAsync(comment)).Returns(Task.CompletedTask);

            // Act
            await _commentService.UpdateCommentAsync(comment);

            // Assert
            _mockCommentRepository.Verify(r => r.UpdateAsync(comment), Times.Once);
        }

        #endregion

        #region GetAllCommentsAsync

        [Test]
        public async Task GetAllCommentsAsync_ShouldReturnFilteredComments()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { Id = 1, Content = "Test Comment 1", OfferId = 1 },
                new Comment { Id = 2, Content = "Test Comment 2", OfferId = 1 }
            };

            Expression<Func<Comment, bool>> filter = c => c.OfferId == 1;
            _mockCommentRepository.Setup(r => r.GetAllAsync(filter)).ReturnsAsync(comments);

            // Act
            var result = await _commentService.GetAllCommentsAsync(filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        #endregion

    }
}
