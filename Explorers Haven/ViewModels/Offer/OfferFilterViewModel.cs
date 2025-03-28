namespace Explorers_Haven.ViewModels.Offer
{
    public class OfferFilterViewModel
    {
        public string? Search{ get; set; }
        public string? filter { get; set; }
        public List<OfferViewModel> Offers { get; set; }

        public List<OfferViewModel> Cheapest_Offers { get; set; }
    }
}
