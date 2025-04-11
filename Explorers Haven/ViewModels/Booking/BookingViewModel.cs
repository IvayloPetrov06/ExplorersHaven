using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        public decimal? PeopleCount { get; set; }
        public decimal? YoungOldPeopleCount { get; set; }
        public int? DurationDays { get; set; }
        public DateOnly? StartDate { get; set; }
        public decimal? Price { get; set; }

        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public int? OfferId { get; set; }
        public string? OfferName { get; set; }
        public string? OfferCoverImage { get; set; }
        public List<Models.Travel> Travels { get; set; }
        public BookingViewModel()
        {
            Travels = new List<Models.Travel>();
        }
    }
}
