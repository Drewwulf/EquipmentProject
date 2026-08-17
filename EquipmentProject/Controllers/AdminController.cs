using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentProject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult SiteSettings()
        {
            var siteSettings = _context.SiteSettings.OrderBy(s=>s.Id).Last();

            var model = new SiteSettingViewModel
            {
                Id = siteSettings.Id,
                ShopName = siteSettings.ShopName,
                ShopDesc = siteSettings.ShopDesc,
                HeaderInfo = siteSettings.HeaderInfo,
                SubHeaderInfo = siteSettings.SubHeaderInfo,
                SocialFacebook = siteSettings.SocialFacebook,
                SocialInstagram = siteSettings.SocialInstagram,
                SocialTelegram = siteSettings.SocialTelegram
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SetSettings(SiteSettingViewModel siteSettings)
        {
            var siteconf = new SiteSettings
            {
                ShopName = siteSettings.ShopName,
                ShopDesc = siteSettings.ShopDesc,
                HeaderInfo = siteSettings.HeaderInfo,
                SubHeaderInfo = siteSettings.SubHeaderInfo,
                SocialFacebook = siteSettings.SocialFacebook,
                SocialInstagram = siteSettings.SocialInstagram,
                SocialTelegram = siteSettings.SocialTelegram
            };

            _context.Add(siteconf);
            _context.SaveChanges();

            return RedirectToAction("SiteSettings");
        }
    }
}
