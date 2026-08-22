using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MyMvcApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CategoryController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: /Category
        public async Task<IActionResult> Category()
        {
            var categories = await _context.Categories
                .OrderBy(x => x.Order)
                .ThenBy(x => x.Id)
                .ToListAsync();

            return View(categories);
        }


        // POST: створення категорії
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories
                    .OrderBy(x => x.Order)
                    .ToListAsync();

                return View("Category", categories);
            }

            string? imagePath = null;

            if (model.ImgPath != null)
            {
                imagePath = await SaveImage(model.ImgPath);
            }

            var category = new Category
            {
                ProductName = model.ProductName,
                ShortDescription = model.ShortDescription,
                ImgPath = imagePath,
                Order = model.Order
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }

        private async Task<string?> SaveImage(string imgPath)
        {
            throw new NotImplementedException();
        }


        // GET: редагування
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            var model = new CategoryViewModel
            {
                Id = category.Id,
                ProductName = category.ProductName,
                ShortDescription = category.ShortDescription,
                Order = category.Order,
                ImgPath = category.ImgPath
            };

            ViewBag.EditMode = true;

            var categories = await _context.Categories
                .OrderBy(x => x.Order)
                .ToListAsync();

            ViewBag.EditCategory = model;

            return View("Index", categories);
        }


        // POST: редагування
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (category == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories
                    .OrderBy(x => x.Order)
                    .ToListAsync();

                ViewBag.EditMode = true;
                ViewBag.EditCategory = model;

                return View("Category", categories);
            }

            category.ProductName = model.ProductName;
            category.ShortDescription = model.ShortDescription;
            category.Order = model.Order;

            // Якщо користувач завантажив нову картинку
            if (model.ImgPath != null)
            {
                DeleteImage(category.ImgPath);

                category.ImgPath = await SaveImage(model.ImgPath);
            }

            _context.Categories.Update(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // POST: видалення
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            DeleteImage(category.ImgPath);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }


        // Збереження картинки
        private async Task<string> SaveImage(IFormFile image)
        {
            string uploadsFolder = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "categories"
            );

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string extension = Path.GetExtension(image.FileName);

            string fileName = Guid.NewGuid().ToString() + extension;

            string filePath = Path.Combine(
                uploadsFolder,
                fileName
            );

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/uploads/categories/" + fileName;
        }


        // Видалення картинки
        private void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            string fileName = Path.GetFileName(imagePath);

            string filePath = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "categories",
                fileName
            );

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}