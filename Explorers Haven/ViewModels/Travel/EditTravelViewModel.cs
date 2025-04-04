namespace Explorers_Haven.ViewModels.Travel
{
    public class EditTravelViewModel
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public int? DurationDays { get; set; }
        public bool Arrival { get; set; }
        public int? TransportId { get; set; }
        public int? UserId { get; set; }
        public int? OfferId { get; set; }
    }
}
