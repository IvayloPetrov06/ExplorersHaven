using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        public int? UserId { get; set; }

        public int? OfferId { get; set; }

        public User? User { get; set; }
        public Offer? Offer { get; set; }
    }
}
