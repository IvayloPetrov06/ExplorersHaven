using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class Stay
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Disc { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int? Stars { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }


        public ICollection<Offer>? Offers { get; set; }
        public ICollection<StayAmenity>? StayAmenities { get; set; }
    }
}
