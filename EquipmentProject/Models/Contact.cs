namespace EquipmentProject.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Schedule { get; set; }
        public int SiteSettingId { get; set; }

        public SiteSettings siteSettings = new SiteSettings();
    }
}
