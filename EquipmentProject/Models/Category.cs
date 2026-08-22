namespace EquipmentProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SubcategoryId { get; set; }
        public string ShortDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string ImgPath { get; set; }
        public int Order {  get; set;
        }
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();



    }
}
