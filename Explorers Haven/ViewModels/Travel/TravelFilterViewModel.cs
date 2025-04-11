using Explorers_Haven.ViewModels.Stay;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Travel
{
    public class TravelFilterViewModel
    {

        public List<TravelViewModel> Travels { get; set; }
        public string? Title { get; set; }
    }
}
