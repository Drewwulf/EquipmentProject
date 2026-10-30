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
                View = "Home",

                TechnicalCharacteristics = p.TechnicalCharacteristics
                ,
                Categories = _context.Categories.Where(x => !x.IsDeleted).ToList()
                ,
                        Products = _context.Products
                        .Where(x => !x.IsDeleted)
                        .Take(10)
                        .ToList()


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
                SubcategoryId = Model.SubcategoryId,
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.TechnicalCharacteristics)
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (product == null)
                return NotFound();

            var categories = await _context.Categories
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Order)
                .ToListAsync();

            var products = await _context.Products
                .Where(x => !x.IsDeleted)
                .Take(10)
                .ToListAsync();

            var model = new ProductViewModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Articul = product.Articul,
                Price = product.Price,
                CategoryId = product.CategoryId.GetValueOrDefault(),
                SubcategoryId = product.SubcategoryId.GetValueOrDefault(),
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                IsNew = product.IsNew,
                IsRecomended = product.IsRecomended,
                IsDeleted = product.IsDeleted,
                ImgPath = product.ImgPath,
                Categories = categories,
                Products = products,
                TechnicalCharacteristics = product.TechnicalCharacteristics
                    ?? new List<TechnicalCharacteristic>(),
                View = "Edit"
            };

            return View("AddProduct", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            product.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("AddProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel Model)
        {
            var product = await _context.Products
                .Include(p => p.TechnicalCharacteristics)
                .FirstOrDefaultAsync(x => x.Id == Model.Id && !x.IsDeleted);

            if (product == null)
                return NotFound();

            product.ProductName = Model.ProductName;
            product.Articul = Model.Articul;
            product.Price = Model.Price;
            product.CategoryId = Model.CategoryId;
            product.SubcategoryId = Model.SubcategoryId;
            product.ShortDescription = Model.ShortDescription;
            product.FullDescription = Model.FullDescription;
            product.IsNew = Model.IsNew;
            product.IsRecomended = Model.IsRecomended;

            if (Model.MainImage != null && Model.MainImage.Length > 0)
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

                product.ImgPath = "/uploads/product/" + fileName;
            }

            _context.Set<TechnicalCharacteristic>()
                .RemoveRange(product.TechnicalCharacteristics);

            product.TechnicalCharacteristics = new List<TechnicalCharacteristic>();

            if (Model.TechnicalCharacteristics != null)
            {
                foreach (var characteristic in Model.TechnicalCharacteristics)
                {
                    if (string.IsNullOrWhiteSpace(characteristic.ParamentName) &&
                        string.IsNullOrWhiteSpace(characteristic.ParamentValue))
                        continue;

                    product.TechnicalCharacteristics.Add(new TechnicalCharacteristic
                    {
                        ProductId = product.Id,
                        ParamentName = characteristic.ParamentName,
                        ParamentValue = characteristic.ParamentValue
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = product.Id });
        }

    }
}
