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
    public class User// : IdentityUser
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

        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

        //*public ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        //*public ICollection<Like>? Likes { get; set; } = new List<Like>();



        //public virtual ICollection<IdentityUserClaim<string>>? Claims { get; set; } // claims
        //public virtual ICollection<IdentityUserLogin<string>>? Logins { get; set; } // user
        //public virtual ICollection<IdentityUserToken<string>>? Tokens { get; set; } // authentication
        //public virtual ICollection<IdentityUserRole<string>>? UserRoles { get; set; } // roles
    }
}
