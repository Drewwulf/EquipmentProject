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
            var category = _context.SiteSettings.OrderByDescending(s => s.Id).Include(site=>site.WhyWes).First();
            var news = _context.Products.Where(p => p.IsDeleted == false).ToList();
            var famous = _context.Categories.Where(s => s.IsDeleted==false).Take(6).ToList();



            var categories = new SiteSettingViewModel
            {
                Products = news,
                ShopName = category.ShopName,
                ShopDesc = category.ShopDesc,
                HeaderInfo = category.HeaderInfo,
                SubHeaderInfo = category.SubHeaderInfo,
                SocialFacebook = category.SocialFacebook,
                SocialInstagram = category.SocialInstagram,
                SocialTelegram = category.SocialTelegram,
                ImgForHeader = category.ImgPathForHeader,
                Images = category.Images,
                ImgForLogo = category.ImgPathForLogo,
                WhyWes = category.WhyWes,
                Categories = famous
            };
            ViewBag.Categories = _context.Categories
     .Where(x => !x.IsDeleted).Take(6)
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
                .Include(x => x.Subcategory).ThenInclude(x=>x.Categories)
                .Include(x => x.TechnicalCharacteristics)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

 
        public async Task<IActionResult> SubcategoryCatalog(int id)
        {
            var catalog = _context.Subcategories.Where(c => c.CategoriesId==id ).Include(c=>c.Categories).ToList();
            var catalogs = new SubCategoryViewModel
            {
                Subcategories = catalog,
                CategoryName = catalog.First().Categories.ProductName
            };
            return View(catalogs);
        }

        public async Task<IActionResult> ProductCatalog(int id)
        {
            var pcatalog = _context.Products.Where(p => p.SubcategoryId == id).Where(s=>s.IsDeleted == false).Include(s=>s.Subcategory).ToList();
            var pcatalogs = new ProductViewModel
            {
                products = pcatalog,
                SubCategoryName = pcatalog.Select(s => s.Subcategory).FirstOrDefault(s => s != null)?.NameSubcategory
            };
            return View(pcatalogs);
        }
        public async Task<IActionResult> AllCategories()
        {
            var categories = await _context.Categories
                .Where(c => c.IsDeleted != true)
                .OrderBy(c => c.ProductName)
                .ToListAsync();

            return View(categories);
        }
    }
}

