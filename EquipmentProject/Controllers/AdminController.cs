using EquipmentProject.Data;
using EquipmentProject.Models;
using EquipmentProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult SiteSettings()
        {
            if (!_context.SiteSettings.Any())
            {
                var siteconf = new SiteSettings
                {
                    ShopName = "Магазин",
                    ShopDesc = "",
                    HeaderInfo = "",
                    SubHeaderInfo = "",
                    SocialFacebook = "",
                    SocialInstagram = "",
                    SocialTelegram = "",
                    Contacts = new List<Contact>()
                };

                _context.Add(siteconf);
                _context.SaveChanges();


                return RedirectToAction("SiteSettings");
            }var siteSettings = _context.SiteSettings.Include(site=>site.Contacts)
    .OrderByDescending(s => s.Id)
    .First();

            var model = new SiteSettingViewModel
            {
                Id = siteSettings.Id,
                ShopName = siteSettings.ShopName,
                ShopDesc = siteSettings.ShopDesc,
                HeaderInfo = siteSettings.HeaderInfo,
                SubHeaderInfo = siteSettings.SubHeaderInfo,
                SocialFacebook = siteSettings.SocialFacebook,
                SocialInstagram = siteSettings.SocialInstagram,
                SocialTelegram = siteSettings.SocialTelegram,
                Contacts = siteSettings.Contacts
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
                SocialTelegram = siteSettings.SocialTelegram,
                Contacts = siteSettings.Contacts
            };

            _context.Add(siteconf);
            _context.SaveChanges();

            return RedirectToAction("SiteSettings");
        }
        [HttpGet]
        public IActionResult DeleteContacts(int id)
        {
            var ctd = _context.Contacts.Find(id);
            ctd.isdeleted = true;
            _context.SaveChanges();
            return RedirectToAction("SiteSettings");
        }
    }
}
