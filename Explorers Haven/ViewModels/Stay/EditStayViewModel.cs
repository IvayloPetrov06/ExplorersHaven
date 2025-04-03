using Microsoft.AspNetCore.Mvc.Rendering;

namespace Explorers_Haven.ViewModels.Stay
{
    public class EditStayViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Disc { get; set; }
        public decimal Price { get; set; }
        public int Stars { get; set; }


        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }

        public int[] SelectedAmenities { get; set; } 
        public IEnumerable<SelectListItem> Amenities { get; set; }

        public Explorers_Haven.Models.Amenity[]? existingAmentities { get; set; }
        public SelectList? UserList { get; set; }

        public int? UserId { get; set; }
    }
}
