using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentProject.Controllers
{
    [Authorize(Roles = "Admin")]
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

           

                var p  = _context.Products.ToList().FirstOrDefault();
            if (p == null)
            {
                var siteconf = new Product
                {
                    ProductName = "",
                    Articul = 123,
                    Price = 123,
                    ShortDescription = "",
                    FullDescription = "",

                    IsNew = false,
                    IsRecomended = false,
                    IsDeleted = false,

                    ImgPath = "12w3",

                    
                };

                _context.Products.Add(siteconf);
                _context.SaveChanges();


                return RedirectToAction("AddProduct");
            }
            var model = new ProductViewModel
            {
                ProductName = p.ProductName,
                Articul = p.Articul,
                Price = p.Price,
                ShortDescription = p.ShortDescription,
                FullDescription = p.FullDescription,

                IsNew = p.IsNew,
                IsRecomended = p.IsRecomended,
                IsDeleted = p.IsDeleted,

                ImgPath = p.ImgPath,

                TechnicalCharacteristics = p.TechnicalCharacteristics
                ,
                Categories = _context.Categories.Where(x => !x.IsDeleted).ToList()



            };


            return View(model);
        }
        [HttpPost]
        public IActionResult StartProduct(ProductViewModel Model)
        {

            var product = new Product
            {
                ProductName = Model.ProductName,
                Articul = Model.Articul,
                CategoryId = Model.CategoryId,
                Price = Model.Price,
                ShortDescription = Model.ShortDescription,
                FullDescription = Model.FullDescription,
                IsNew = Model.IsNew,
                IsRecomended = Model.IsRecomended,
                ImgPath = "img.png",
                TechnicalCharacteristics = Model.TechnicalCharacteristics
            };

            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("AddProduct");

        }
    }
}
