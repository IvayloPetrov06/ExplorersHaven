using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Trip
{
    public class TripViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public List<Models.Trip> Trips { get; set; }
    }
}
