using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EquipmentProject.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var category = _context.Categories.ToList();

            var subcat = _context.Subcategories
                .Include(s => s.Categories)
                .ToList();

            var model = new SubCategoryViewModel
            {
                Categories = category,
                Subcategories = subcat,
                View = "Create"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var subcategory = await _context.Subcategories.Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (subcategory == null)
                return NotFound();

            // М'яке видалення
            subcategory.IsDeleted = true;

            // Позначаємо об'єкт як змінений
            _context.Subcategories.Update(subcategory);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var subcategory = _context.Subcategories
                .FirstOrDefault(s => s.Id == id);

            if (subcategory == null)
            {
                return NotFound();
            }

            var model = new SubCategoryViewModel
            {
                Id = subcategory.Id,
                SubcategoryId = subcategory.SubcategoryId,
                NameSubcategory = subcategory.NameSubcategory,
                ShortDescription = subcategory.ShortDescription,
                IsDeleted = subcategory.IsDeleted,
                ImgPath = subcategory.ImgPath,
                Order = subcategory.Order,
                View = "Edit",

                Categories = _context.Categories
                    .Where(c => !c.IsDeleted)
                    .ToList()
            };

            if (subcategory.Categories != null)
            {
                model.CategoryId = subcategory.Categories.Id;
            }

            return View("Index", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubCategoryViewModel model)
        { 

            var subcategory = await _context.Subcategories
                .FirstOrDefaultAsync(s => s.Id == model.Id);

            if (subcategory == null)
            {
                return NotFound();
            }

            subcategory.NameSubcategory = model.NameSubcategory;
            subcategory.ShortDescription = model.ShortDescription;
            subcategory.Order = model.Order;

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == model.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Категорію не знайдено.");

                model.Categories = _context.Categories
                    .Where(c => !c.IsDeleted)
                    .ToList();

                model.Subcategories = _context.Subcategories.ToList();

                return View("Index", model);
            }

            subcategory.Categories = category;

            if (model.MainImage != null)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "subcategories"
                );

                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() +
                               Path.GetExtension(model.MainImage.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.MainImage.CopyToAsync(stream);
                }

                subcategory.ImgPath = "/uploads/subcategories/" + fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create(SubCategoryViewModel model)
        {

            var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "subcategories"
                );

            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + 
                           Path.GetExtension(model.MainImage.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.MainImage.CopyToAsync(stream);
            }

            var ImgPaths = "/uploads/subcategories/" + fileName;

            var subcategory = new Subcategory
            {
                NameSubcategory = model.NameSubcategory,
                CategoriesId = model.CategoryId,
                ShortDescription = model.ShortDescription,
                Order = model.Order,
                ImgPath = ImgPaths,
                IsDeleted =false
            };

            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}