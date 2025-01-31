using System.ComponentModel.DataAnnotations;

namespace Explorers_Haven.Models
{
    public class StayViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public List<Stay> Stays { get; set; }
    }
}
