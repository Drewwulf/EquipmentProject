using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EquipmentProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private bool isDeleted;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var category = _context.SiteSettings.OrderByDescending(s => s.Id).First();
            var news = _context.Products.Where(x => !x.IsDeleted).ToList();




            var categories = new SiteSettingViewModel
            {
                Products = news,
                ShopName = category.ShopName,
                ShopDesc = category.ShopDesc,
                HeaderInfo = category.HeaderInfo,
                SubHeaderInfo = category.SubHeaderInfo,
                SocialFacebook = category.SocialFacebook,
                SocialInstagram = category.SocialInstagram,
                SocialTelegram = category.SocialTelegram
            };
            ViewBag.Categories = _context.Categories
     .Where(x => !x.IsDeleted)
     .OrderBy(x => x.Order)
     .ToList();
            return View(categories);
        }

   

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ProductInfo(int id)
        {
            var product = _context.Products
                .Include(x => x.Subcategory)
                .Include(x => x.TechnicalCharacteristics)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
