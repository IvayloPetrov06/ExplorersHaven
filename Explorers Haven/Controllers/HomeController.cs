using Explorers_Haven.Core.IServices;
using Explorers_Haven.ViewModels.Offer;
using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOfferService _offerService;
        public HomeController(IOfferService offerService)
        {
            _offerService = offerService;
        }
        public async Task<IActionResult> Index(OfferFilterViewModel? filter)
        {
            var query = _offerService.GetAll().AsQueryable();
            var filterModel = new OfferFilterViewModel();

            if (string.IsNullOrEmpty(filter.Search))
            {

                var model = _offerService.CombinedInclude().Select(x => new OfferViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CoverImage = x.CoverImage
                }).ToList();

                filterModel = new OfferFilterViewModel
                {
                    Offers = model,
                    Search = filter.Search,

                };
            }
            else
            {
                var tempOffers = await _offerService.GetAllOfferNamesAsync();
                if (tempOffers.Contains(filter.Search))
                {
                    query = query.Where(x => x.Name == filter.Search);
                }

                filterModel = new OfferFilterViewModel
                {
                    Offers = query
                .Select(x => new OfferViewModel()
                {
                    Name = x.Name,
                    CoverImage = x.CoverImage,
                    Id = x.Id,
                }).ToList(),
                    Search = filter.Search
                };
            };

            return View(filterModel);
        }
    }
}
