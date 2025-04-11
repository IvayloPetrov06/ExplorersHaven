using Explorers_Haven.Core.Services;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.DataAccess;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Explorers_Haven.Tests
{
    [TestFixture]
    public class OfferServiceTest
    {
        private Mock<IRepository<Offer>> _mockOfferRepository;
        private IOfferService _offerService;

        [SetUp]
        public void Setup()
        {
            _mockOfferRepository = new Mock<IRepository<Offer>>();
            _offerService = new OfferService(_mockOfferRepository.Object);
        }

        [Test]
        public async Task AddOfferAsync()
        {
            var offers = new List<Offer>();
            var offer = new Offer { Id = 1, Name = "Offer 1", StayId = 1 };
            _mockOfferRepository.Setup(r => r.AddAsync(offer)).Callback(() => offers.Add(offer));

            await _offerService.AddOfferAsync(offer);

            Assert.AreEqual(1, offers.Count);
        }

        [Test]
        public async Task DeleteOfferAsync()
        {
            var offer = new Offer { Id = 1, Name = "Offer 1", StayId = 1 };
            var offers = new List<Offer> { offer };
            _mockOfferRepository.Setup(r => r.DeleteAsync(offer)).Callback(() => offers.Remove(offer));

            await _offerService.DeleteOfferAsync(offer);

            Assert.AreEqual(0, offers.Count);
        }

        [Test]
        public async Task DeleteOfferByIdAsync()
        {
            _mockOfferRepository.Setup(r => r.DeleteByIdAsync(1)).Returns(Task.CompletedTask);

            Assert.DoesNotThrowAsync(async () => await _offerService.DeleteOfferByIdAsync(1));
        }

        [Test]
        public async Task GetOfferByIdAsync()
        {
            int offerId = 1;
            var offer = new Offer { Id = offerId, Name = "Offer 1", StayId = 1 };
            _mockOfferRepository.Setup(r => r.GetByIdAsync(offerId)).ReturnsAsync(offer);

            var result = await _offerService.GetOfferByIdAsync(offerId);

            Assert.AreEqual(offer, result);
        }

        [Test]
        public async Task GetOfferAsync()
        {
            var offer = new Offer { Id = 1, Name = "Offer 1", StayId = 1 };
            _mockOfferRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Offer, bool>>>()))
                .ReturnsAsync(offer);

            var result = await _offerService.GetOfferAsync(o => o.Id == 1);

            Assert.AreEqual(offer, result);
        }

        [Test]
        public void GetAll()
        {
            var offers = new List<Offer>
            {
                new Offer { Id = 1, Name = "Offer 1", StayId = 1 },
                new Offer { Id = 2, Name = "Offer 2", StayId = 1 }
            }.AsQueryable();

            _mockOfferRepository.Setup(r => r.GetAll()).Returns(offers);

            var result = _offerService.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAllOffersAsync()
        {
            var offers = new List<Offer>
            {
                new Offer { Id = 1, Name = "Offer 1", StayId = 1 },
                new Offer { Id = 2, Name = "Offer 2", StayId = 1 }
            };

            _mockOfferRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(offers);

            var result = await _offerService.GetAllOfferAsync();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAllOffersAsyncWithFilter()
        {
            var offers = new List<Offer>
            {
                new Offer { Id = 1, Name = "Offer 1", StayId = 1 },
                new Offer { Id = 2, Name = "Offer 2", StayId = 1 }
            };

            _mockOfferRepository.Setup(r => r.GetAllAsync(It.IsAny<Expression<Func<Offer, bool>>>()))
                .ReturnsAsync((Expression<Func<Offer, bool>> filter) => offers.Where(filter.Compile()).ToList());

            var result = await _offerService.GetAllOfferAsync(o => o.Id == 1);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(1, result.First().Id);
        }

        [Test]
        public async Task UpdateOfferAsync()
        {
            var offer = new Offer { Id = 1, Name = "Offer 1", StayId = 1 };
            _mockOfferRepository.Setup(r => r.UpdateAsync(offer)).Returns(Task.CompletedTask);

            Assert.DoesNotThrowAsync(async () => await _offerService.UpdateOfferAsync(offer));
        }

        [Test]
        public void AllWithInclude()
        {
            var offers = new List<Offer>
            {
                new Offer { Id = 1, Name = "Offer 1", StayId = 1 },
                new Offer { Id = 2, Name = "Offer 2", StayId = 1 }
            }.AsQueryable();

            _mockOfferRepository.Setup(r => r.GetAllQuery()).Returns(offers);

            var result = _offerService.CombinedInclude();

            Assert.AreEqual(2, result.Count());
        }
    }
}
