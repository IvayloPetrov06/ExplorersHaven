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
        public string? Disc { get; set; }
        public string? CoverImage { get; set; }
        public string? BackImage { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rating { get; set; }
        public int? Clicks { get; set; }
        public int? UserId { get; set; }
        public int? StayId { get; set; }
        public User? User { get; set; }
        public Stay? Stay { get; set; }

        public ICollection<Models.Activity>? Activities { get; set; }
        public ICollection<Travel>? Travels { get; set; }
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

    }
}
