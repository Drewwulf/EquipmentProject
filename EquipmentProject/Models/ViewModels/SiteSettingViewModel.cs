namespace EquipmentProject.Models.ViewModels
{
    public class SiteSettingViewModel
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string ShopDesc { get; set; }
        public string HeaderInfo { get; set; }
        public string SubHeaderInfo { get; set; }
        public string SocialFacebook { get; set; }
        public string SocialInstagram { get; set; }
        public string SocialTelegram { get; set; }
        public string SocialTikTok { get; set; }
        public string SocialYouTube { get; set; }
        public string SocialWhatsApp { get; set; }
        public string ImgForLogo { get; set; }
        public IFormFile MainImageForLogo { get; set; }
        public string ImgForHeader { get; set; }
        public IFormFile MainImageForHeader { get; set; }

        public List<IFormFile> ImagesIFormFile { get; set; } = new();
        public List<Images> Images { get; set; } = new();
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<WhyWe> WhyWes { get; set; } = new List<WhyWe>();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
