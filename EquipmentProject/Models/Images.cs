namespace EquipmentProject.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string? ImgPath { get; set; }
        public int SiteSettingsId { get; set; } = 1;
        public bool IsDeleted { get; set; } = false;
        public SiteSettings siteSettings = new SiteSettings();
    }
}
