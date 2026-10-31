namespace EquipmentProject.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string? ImgPath { get; set; }
        public int? SiteSettingsId { get; set; }
        public int? ProductId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public SiteSettings siteSettings = new SiteSettings();
        public Product Products = new Product();
    }
}
