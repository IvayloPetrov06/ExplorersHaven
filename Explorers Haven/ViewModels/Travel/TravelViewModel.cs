using System.ComponentModel.DataAnnotations;
namespace Explorers_Haven.ViewModels.Travel
{
    public class TravelViewModel
    {
        public int? Id { get; set; }
        public string? Start { get; set; }
        public string? Finish { get; set; }
        public string? Transport { get; set; }

        public List<Models.Travel> Travels { get; set; }
    }
}
