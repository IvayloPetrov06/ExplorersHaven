using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.Models.Activity
{
    public class ActivityViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public SelectList? Trips { get; set; }
        public int? TripId { get; set; }
        public List<ActivityViewModel> Activities { get; set; }
    }
}
