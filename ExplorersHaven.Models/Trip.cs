using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Offer))]
        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        public ICollection<Models.Activity>? Activities { get; set; }

        //[ForeignKey(nameof(Stay))]
        //public int? StayId { get; set; }
        public Stay? Stay { get; set; }

        //[ForeignKey(nameof(Travel))]
        //public int TravelId { get; set; }
        public Travel? Travel { get; set; }


    }
}
