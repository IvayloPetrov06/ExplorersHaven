using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Models
{
    public class TravelogueViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public Decimal? MinPrice { get; set; }

        public Decimal? MaxPrice { get; set; }

        public List<Travelogue> Travelogues { get; set; }
    }
}
