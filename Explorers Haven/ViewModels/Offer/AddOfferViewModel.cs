using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Offer
{
    public class AddOfferViewModel
    {
        public string Name { get; set; }

        public int? StayId { get; set; }
        public decimal? Price { get; set; }
        public IFormFile? Picture { get; set; }
        public IFormFile? BackPicture { get; set; }
    }
}
