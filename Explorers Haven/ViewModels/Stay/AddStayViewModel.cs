using Explorers_Haven.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Stay
{
    public class AddStayViewModel
    {
        public string Title { get; set; }
        public string Disc { get; set; }
        public string Price { get; set; }
        public int Stars { get; set; }
        public IFormFile? imageFile { get; set; }
        public string? ImageLink { get; set; }

        public int[] SelectedAmenities { get; set; } 
        public IEnumerable<SelectListItem> Amenities { get; set; }

        public Explorers_Haven.Models.Amenity[]? existingAmentities { get; set; }
    }
}
