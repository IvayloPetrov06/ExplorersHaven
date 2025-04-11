using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Stay
{
    public class StayFilterViewModel
    {
        
        public int? StarValue { get; set; }

        public List<StayViewModel> Stays { get; set; }
        public string? Title { get; set; }
        public int? Price { get; set; }
        
    }
}
