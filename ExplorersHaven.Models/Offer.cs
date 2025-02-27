using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Explorers_Haven.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? CoverImage { get; set; }
        public decimal? Price { get; set; }
        public ICollection<Trip>? Trips { get; set; }

    }
}
