using EquipmentProject.Data;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EquipmentProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context; // ваш DbContext

        public DashboardController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                CategoriesCount = await _context.Categories.CountAsync(),
                SubcategoriesCount = await _context.Subcategories.CountAsync(),
                ProductsCount = await _context.Products.CountAsync()
            };
            return View(vm);
        }

    }
}