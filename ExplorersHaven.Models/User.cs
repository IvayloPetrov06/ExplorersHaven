using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Explorers_Haven.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string Password { get; set; }
        public string? ProfilePicture { get; set; }
        [MaxLength(500)]
        public string? Bio { get; set; }

        public IdentityUser UserIdentity { get; set; }
        public string UserIdentityId { get; set; }

        public ICollection<Offer>? Offers { get; set; } = new List<Offer>();
        public ICollection<Travel>? Travels { get; set; } = new List<Travel>();
        public ICollection<Activity>? Activities { get; set; } = new List<Activity>();
        public ICollection<Stay>? Stays { get; set; } = new List<Stay>();
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
    }
}
