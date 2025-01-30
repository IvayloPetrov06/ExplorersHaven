using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.Controllers
{
    public class TripController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
