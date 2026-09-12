namespace EquipmentProject.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Address { get; set; } = "Adress";
        public string PhoneNumber { get; set; } = "+380123456789";
        public string Email { get; set; } = "Compane@company.com";
        public string Schedule { get; set; } = "Schedule";
        public int SiteSettingsId { get; set; } = 1;
        public bool isdeleted { get; set; } = false;
        public SiteSettings siteSettings = new SiteSettings();
    }
}
