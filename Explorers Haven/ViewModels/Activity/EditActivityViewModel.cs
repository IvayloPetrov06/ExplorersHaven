namespace Explorers_Haven.ViewModels.Activity
{
    public class EditActivityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? CoverImage { get; set; }
        public IFormFile? Picture { get; set; }
        public int? UserId { get; set; }
        public int? OfferId { get; set; }
    }
}
