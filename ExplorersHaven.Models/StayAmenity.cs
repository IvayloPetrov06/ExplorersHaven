using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class StayAmenity
    {
        [Key]
        public int Id { get; set; }

        public int? AmenityId { get; set; }

        public int? StayId { get; set; }

        public Amenity? Amenity { get; set; }
        public Stay? Stay { get; set; }
    }
}
