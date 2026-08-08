using Microsoft.AspNetCore.Mvc;

namespace EquipmentProject.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
            
        }
    }
}
