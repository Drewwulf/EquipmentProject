using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EquipmentProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var category = _context.SiteSettings.OrderByDescending(s => s.Id).First();
            var news = _context.Products.Where(p => p.IsNew == true).ToList();




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
    }
}
