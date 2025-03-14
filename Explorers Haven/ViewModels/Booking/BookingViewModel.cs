namespace Explorers_Haven.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public int? OfferId { get; set; }
        public string? OfferName { get; set; }
    }
}
