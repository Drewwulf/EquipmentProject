namespace EquipmentProject.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string ProductName { get; set; }
        public int SubcategoryId { get; set; }
        public string ShortDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImgPath { get; set; }
        public int Order { get; set; }
        public int Id { get; internal set; }
    }
}