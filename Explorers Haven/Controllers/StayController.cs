using Microsoft.AspNetCore.Mvc;

namespace Explorers_Haven.Controllers
{
    public class StayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
