using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class Transport
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public ICollection<Travel>? Travels { get; set; }

    }
}
