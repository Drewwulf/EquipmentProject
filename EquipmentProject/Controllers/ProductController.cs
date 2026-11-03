using AspNetCoreGeneratedDocument;
using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Resources;
using static System.Net.Mime.MediaTypeNames;

namespace EquipmentProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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
            var ImgPaths = await SaveImageAsync(Model.MainImage, null, "products");

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

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (Model.GalleryImages != null)
            {
                foreach (var img in Model.GalleryImages)
                {
                    if (img == null || img.Length == 0)
                        continue;

                    var imagePath = await SaveImageAsync(
                        img,
                        null,
                        "products"
                    );

                    var imageI = new Images()
                    {
                        ImgPath = imagePath,
                        ProductId = product.Id
                    };

                    _context.Images.Add(imageI);
                }

                await _context.SaveChangesAsync();
            }

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
                .Include(p => p.Images)
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
                Images = product.Images.Where(p => !p.IsDeleted).ToList(),
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
            return RedirectToAction("Edit", new { id = product.Id });
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

            if (Model.GalleryImages != null)
            {
                foreach (var img in Model.GalleryImages)
                {
                    if (img == null || img.Length == 0)
                        continue;

                    var imagePath = await SaveImageAsync(
                        img,
                        null,
                        "products"
                    );

                    var imageI = new Images()
                    {
                        ImgPath = imagePath,
                        ProductId = product.Id
                    };

                    _context.Images.Add(imageI);
                }

                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = product.Id });
        }

        private async Task<string?> SaveImageAsync(
            IFormFile? file,
            string? oldPath,
            string folder)
        {
            if (file == null || file.Length == 0)
                return oldPath;

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                folder
            );

            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString()
                + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{fileName}";
        }

        public IActionResult DeleteImage(int id)
        {
            var image = _context.Images.Where(i => i.Id == id).FirstOrDefault();
            image.IsDeleted = true;
            _context.SaveChanges();
            DeleteImage(image.ImgPath);
            return RedirectToAction("AddProduct");
        }

        private void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            string fileName = Path.GetFileName(imagePath);

            string filePath = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "header",
                fileName
            );

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
