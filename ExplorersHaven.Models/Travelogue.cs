﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class Travelogue
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int? Price { get; set; }
        public ICollection<Trip>? Trips { get; set; }

    }
}
