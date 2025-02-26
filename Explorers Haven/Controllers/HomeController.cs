using Explorers_Haven.Core.IServices;
using Explorers_Haven.Models;
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
        public IActionResult HomePage(OfferViewModel? filter)
        {

            var query = _offerService.GetAll().AsQueryable();
            
            if (filter.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            var model = new OfferViewModel
            {
                Id = filter.Id,
                Name = filter.Name,

                MinPrice = filter.MinPrice,
                MaxPrice = filter.MaxPrice,
                Offers = query.ToList()
            };

            return View(model);
        }
    }
}
