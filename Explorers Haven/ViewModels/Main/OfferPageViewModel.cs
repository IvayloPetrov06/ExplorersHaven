﻿using Explorers_Haven.ViewModels.Activity;
using Explorers_Haven.ViewModels.Amenity;
using Explorers_Haven.ViewModels.Offer;
using Explorers_Haven.ViewModels.Travel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Main
{
    public class OfferPageViewModel
    {
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public decimal? OfferPrice { get; set; }
        public string? OfferPic { get; set; }
        public string OfferDisc { get; set; }

        public string? StayName { get; set; }
        public string? StayDisc { get; set; }
        public decimal? StayPrice { get; set; }
        public int? StayStars { get; set; }
        public string? StayPic { get; set; }

        public List<Models.Amenity> Amenities { get; set; }
        public List<Models.Activity> Activities { get; set; }
        public List<Models.Travel> Travels { get; set; }

        public IFormFile? ImageFileOfferCover { get; set; }
        public SelectList? UserList { get; set; }
        public int? UserId { get; set; }
        public OfferPageViewModel()
        {
            Activities = new List<Models.Activity>(); // Initialize the list
            Travels = new List<Models.Travel>();
            Amenities = new List<Models.Amenity>();
        }
    }
}
