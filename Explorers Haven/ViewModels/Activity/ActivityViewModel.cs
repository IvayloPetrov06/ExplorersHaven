using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Activity
{
    public class ActivityViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public string UserName { get; set; }
        public string OfferName { get; set; }
        public string? ImageLink { get; set; }
        public List<Models.Activity> Activities { get; set; }
    }
}
