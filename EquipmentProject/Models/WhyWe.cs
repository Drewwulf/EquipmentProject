using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentProject.Models
{
    public class WhyWe
    {
        public int Id { get; set; }
        public string header { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
        public string subheader { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
        public string? ImgPath { get; set; }

        [NotMapped]
        public IFormFile? MainImage { get; set; }
        public bool isDeleted { get; set; } = false;

        public int sitesettingId { get; set; } = 1;
        public SiteSettings siteSettings = new SiteSettings();
    }
}
