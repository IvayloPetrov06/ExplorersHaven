using Explorers_Haven.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Explorers_Haven.Controllers
{
    public class ApplicationController : Controller
    {
        //private readonly IProductService _productService;
        //private readonly ICategoryService _categoryService;

        private readonly ApplicationDbContext _db;
        public ApplicationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
