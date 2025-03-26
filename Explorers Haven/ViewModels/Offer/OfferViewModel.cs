using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Offer
{
    public class OfferViewModel
    {

        public int OfferId { get; set; }
        public string OfferName { get; set; }//OfferRating
        public decimal? OfferPrice { get; set; }
        public decimal? OfferDays { get; set; }//free

        public DateOnly? OfferStart { get; set; }
        public DateOnly? OfferLast { get; set; }
        public decimal? OfferPeople { get; set; }//
        public string? OfferPic { get; set; }
        public string? OfferDisc { get; set; }
        public decimal? OfferRatingStars { get; set; }
        public decimal? OfferRating { get; set; }//IsFavorited
        public bool? IsFavorited { get; set; }
        public bool? IsBooked { get; set; }
        public bool? IsOnDiscount { get; set; }
        public decimal? OfferDiscount { get; set; }
        public string? UserName { get; set; }

        public string? StayName { get; set; }
        public string? StayDisc { get; set; }
        public decimal? StayPrice { get; set; }
        public decimal? StayStars { get; set; }
        public string? StayPic { get; set; }

        public List<Models.Amenity>? Amenities { get; set; }
        public List<Models.Activity>? Activities { get; set; }
        public List<Models.Travel>? Travels { get; set; }
        public List<Models.Transport>? Transports { get; set; }
        public List<Models.Comment>? Comments { get; set; }
        public List<Models.Rating>? Ratings { get; set; }
        public List<Models.User>? Users { get; set; }

        public IFormFile? ImageFileOfferCover { get; set; }
        public SelectList? UserList { get; set; }
        public int? UserId { get; set; }
        public OfferViewModel()
        {
            Activities = new List<Models.Activity>(); // Initialize the list
            Travels = new List<Models.Travel>();
            Transports = new List<Models.Transport>();
            Ratings = new List<Models.Rating>();
            Comments = new List<Models.Comment>();
            Amenities = new List<Models.Amenity>();
            Users = new List<Models.User>();
        }

    }
}
