using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorersHaven.Models
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Start { get; set; }
        public string End { get; set; }
        public string? Transport { get; set; }


        [ForeignKey(nameof(Trip))]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
