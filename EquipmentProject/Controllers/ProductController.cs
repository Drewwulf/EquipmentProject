using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentProject.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
            
        }
        [HttpPost]
        public IActionResult StartProduct(ProductViewModel Model)
        {

            var product = new Product
            {
                ProductName = Model.ProductName,
                Articul = Model.Articul,
                Price = Model.Price,
                ShortDescription = Model.ShortDescription,
                FullDescription = Model.FullDescription,
                IsNew = Model.IsNew,
                IsRecomended = Model.IsRecomended,
                ImgPath = "img.png"
            };

            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("AddProdcut");

        }
    }
}
