using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Start { get; set; }
        public string Finish { get; set; }
        public int? DurationDays { get; set; }
        public bool? Arrival { get; set; }

        public DateOnly? DateStart { get; set; }
        public DateOnly? DateFinish { get; set; }
        public int TransportId { get; set; }
        public Transport Transport { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
