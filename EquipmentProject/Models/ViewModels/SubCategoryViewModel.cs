namespace EquipmentProject.Models.ViewModels
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int SubcategoryId { get; set; }

        public string NameSubcategory { get; set; }

        public string ShortDescription { get; set; }

        public bool IsDeleted { get; set; }

        public string ImgPath { get; set; }

        public int Order { get; set; }

        public IFormFile MainImage { get; set; }

        public List<Subcategory> Subcategories { get; set; }

        public List<Category> Categories { get; set; }

        public List<Product> Products { get; set; }
    }
}