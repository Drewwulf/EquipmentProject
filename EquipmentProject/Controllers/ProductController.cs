using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Resources;

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
        public async Task<IActionResult> StartProduct(ProductViewModel Model)
        {
            var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "product"
                );

            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + 
                           Path.GetExtension(Model.MainImage.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Model.MainImage.CopyToAsync(stream);
            }

            var ImgPaths = "/uploads/product/" + fileName;


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
                ImgPath = ImgPaths,
                TechnicalCharacteristics = Model.TechnicalCharacteristics
            };

            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("AddProduct");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.IsDeleted = false;

            await _context.SaveChangesAsync();

            return RedirectToAction("DeletedData", "Admin");
        }



    }
}
