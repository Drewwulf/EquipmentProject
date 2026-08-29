using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentProject.Controllers
{
    public class SubCategoryController : Controller
    {
        private ApplicationDbContext _context;
        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new SubCategoryViewModel { };
            return View(model);
        }
    }
}
