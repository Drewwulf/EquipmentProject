namespace EquipmentProject.Models
{
    public class SiteSettings
    {
        public int Id {  get; set; }
        public string ShopName { get; set; } = "shop";
        public string ShopDesc { get; set; } = "Shop Description";
        public string HeaderInfo { get; set; } = "Header Info";
        public string? SubHeaderInfo { get; set; } = "SubHeader Info";
        public string? SocialFacebook { get; set; } = string.Empty;
        public string? SocialInstagram { get; set; } = string.Empty;
        public string? SocialTelegram { get; set; } = string.Empty;
        public string? ImgPath { get; set; }

        public List<Contact> Contacts { get; set; }= new List<Contact>();
        public List<WhyWe> WhyWes { get; set; } = new List<WhyWe>();

    }
}
 