﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplorersHaven.Models
{
    public class Stay
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HotelName { get; set; }

        [ForeignKey(nameof(Trip))]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
    }
}
