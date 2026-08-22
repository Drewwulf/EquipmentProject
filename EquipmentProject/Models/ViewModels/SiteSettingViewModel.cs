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

        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
