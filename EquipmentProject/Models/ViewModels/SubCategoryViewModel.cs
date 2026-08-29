namespace EquipmentProject.Models
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public int SubcategoryId { get; set; }
        public string NameSubcategory { get; set; }

        public string ShortDescription { get; set; }
        public bool IsDeleted { get; set; }
        public string ImgPath { get; set; }
        public int Order { get; set; }

        public List<Subcategory> Subcategories { get; set; }

        public Category Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
