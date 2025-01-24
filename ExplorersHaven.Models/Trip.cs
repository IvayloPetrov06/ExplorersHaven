using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorersHaven.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Travelogue))]
        public int TravelogueId { get; set; }
        public Travelogue Travelogue { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public Stay Stay { get; set; }
        public Travel Travel { get; set; }


    }
}
