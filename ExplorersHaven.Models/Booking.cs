using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorers_Haven.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public decimal? PeopleCount {  get; set; }
        public int? YoungOldPeopleCount { get; set; }
        public DateOnly? StartDate { get; set; }
        public decimal? Price { get; set; }
        public string? OfferName { get; set; }
        /*public int? MaxPeople { get; set; }
        public int? Discount { get; set; }
        public int? DurationDays { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? LastDate { get; set; }*/
        public int? UserId { get; set; }

        public int? OfferId { get; set; }

        public User? User { get; set; }
        public Offer? Offer { get; set; }
    }
}
