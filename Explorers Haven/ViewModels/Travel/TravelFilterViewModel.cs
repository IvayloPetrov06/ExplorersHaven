using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Travel
{
    public class TravelFilterViewModel
    {
        //public SelectList? Genres { get; set; }
        //public int? StarValue { get; set; }

        public List<TravelViewModel> Travels { get; set; }
        public string? Title { get; set; }
        //public int? Price { get; set; }
    }
}
