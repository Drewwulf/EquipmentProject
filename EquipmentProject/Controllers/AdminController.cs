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
                    Contacts = new List<Contact>(),
                    WhyWes = new List<WhyWe>()
                };

                _context.Add(siteconf);
                _context.SaveChanges();


                return RedirectToAction("SiteSettings");
            }var siteSettings = _context.SiteSettings.Include(site=>site.Contacts).Include(site=>site.WhyWes)
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
                Contacts = siteSettings.Contacts,
                WhyWes = siteSettings.WhyWes
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetSettings(SiteSettingViewModel siteSettings)
        {
            var siteconf = await _context.SiteSettings
                .Include(x => x.Contacts)
                .Include(x => x.WhyWes)
                .FirstOrDefaultAsync();

            if (siteconf == null)
            {
                siteconf = new SiteSettings();

                _context.SiteSettings.Add(siteconf);
            }

            siteconf.ShopName = siteSettings.ShopName;
            siteconf.ShopDesc = siteSettings.ShopDesc;
            siteconf.HeaderInfo = siteSettings.HeaderInfo;
            siteconf.SubHeaderInfo = siteSettings.SubHeaderInfo;

            siteconf.SocialFacebook = siteSettings.SocialFacebook;
            siteconf.SocialInstagram = siteSettings.SocialInstagram;
            siteconf.SocialTelegram = siteSettings.SocialTelegram;

            siteconf.Contacts = siteSettings.Contacts;

            if (siteSettings.WhyWes != null)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "whywes"
                );

                Directory.CreateDirectory(uploadsFolder);

                foreach (var whyWe in siteSettings.WhyWes)
                {
                    if (whyWe.MainImage != null &&
                        whyWe.MainImage.Length > 0)
                    {
                        var fileName =
                            Guid.NewGuid().ToString() +
                            Path.GetExtension(
                                whyWe.MainImage.FileName
                            );

                        var filePath = Path.Combine(
                            uploadsFolder,
                            fileName
                        );

                        using (var stream = new FileStream(
                            filePath,
                            FileMode.Create))
                        {
                            await whyWe.MainImage.CopyToAsync(stream);
                        }

                        whyWe.ImgPath =
                            "/uploads/whywes/" + fileName;
                    }
                }

                siteconf.WhyWes = siteSettings.WhyWes;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(SiteSettings));
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
