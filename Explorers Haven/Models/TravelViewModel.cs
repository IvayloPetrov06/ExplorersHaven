using System.ComponentModel.DataAnnotations;
namespace Explorers_Haven.Models
{
    public class TravelViewModel
    {
        public int? Id { get; set; }
        public string? Start { get; set; }
        public string? Finish { get; set; }
        public string? Transport { get; set; }

        public List<Travel> Travels { get; set; }
    }
}
