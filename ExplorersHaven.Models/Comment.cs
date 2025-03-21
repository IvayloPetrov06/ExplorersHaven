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

        [Range(1, 5, ErrorMessage = "Value must be between 1 and 5.")]
        public int Stars { get; set; }

        public int? UserId { get; set; }

        public int? OfferId { get; set; }

        public User? User { get; set; }
        public Offer? Offer { get; set; }
    }
}
