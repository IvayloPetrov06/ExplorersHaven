using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Main
{
    public class OfferPageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal? Price { get; set; }
        public string? OfferCover { get; set; }
        public string? StayName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public SelectList? UserList { get; set; }
        public int? UserId { get; set; }
    }
}
