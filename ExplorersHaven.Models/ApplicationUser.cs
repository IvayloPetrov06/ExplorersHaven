using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity;
using Microsoft.AspNetCore.Identity;

namespace Explorers_Haven.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<IdentityUserClaim<string>>? Claims { get; set; } // claims
        public virtual ICollection<IdentityUserLogin<string>>? Logins { get; set; } // user
        public virtual ICollection<IdentityUserToken<string>>? Tokens { get; set; } // authentication
        public virtual ICollection<IdentityUserRole<string>>? UserRoles { get; set; } // roles
    }
}
