using Explorers_Haven.ViewModels.Booking;

namespace Explorers_Haven.ViewModels.Booking
{
    public class BookingFilterViewModel
    {
        public string Search { get; set; }
        public List<BookingViewModel> Bookings { get; set; }
        public List<Models.Transport> Transports { get; set; }
    }
}
