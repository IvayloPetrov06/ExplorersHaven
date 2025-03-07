using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Activity
{
    public class ActivityViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public SelectList? Offers { get; set; }
        public int? OfferId { get; set; }
        public List<Models.Activity> Activities { get; set; }
    }
}
