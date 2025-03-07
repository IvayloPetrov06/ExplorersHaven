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


        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
