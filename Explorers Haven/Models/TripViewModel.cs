using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Models
{
    public class TripViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public List<Trip> Trips { get; set; }
    }
}
