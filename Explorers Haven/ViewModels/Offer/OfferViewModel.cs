using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Offer
{
    public class OfferViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public decimal? Price { get; set; }
        public string? CoverImage { get; set; }

    }
}
