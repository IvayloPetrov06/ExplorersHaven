using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.ViewModels.User
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicture { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
