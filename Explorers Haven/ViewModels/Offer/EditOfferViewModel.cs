using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Offer
{
    public class EditOfferViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? MaxPeople { get; set; }
        public decimal? Discount { get; set; }
        public int? DurationDays { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? LastDate { get; set; }
        public string? Disc { get; set; }
        public string? CoverImage { get; set; }
        public string? BackImage { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rating { get; set; }
        public decimal? RealRating { get; set; }

        public int? StayId { get; set; }
        public int? UserId { get; set; }
        public IFormFile? Picture { get; set; }
        public IFormFile? BackPicture { get; set; }
    }
}
