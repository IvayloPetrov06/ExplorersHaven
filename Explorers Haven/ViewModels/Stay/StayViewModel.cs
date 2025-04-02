using System.ComponentModel.DataAnnotations;

namespace Explorers_Haven.ViewModels.Stay
{
    public class StayViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public List<Models.Stay> Stays { get; set; }
        public decimal? Price { get; set; }
        public int? Stars { get; set; }
        public string? ImageLink { get; set; }
        public string UserName { get; set; }
        
    }
}
