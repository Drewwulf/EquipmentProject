namespace EquipmentProject.Models.ViewModels
{
    public class ProductViewModel
    {
       
        public string ProductName { get; set; }
        public int Id
        public int Articul { get; set; }
        public int Price { get; set; }  

        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool IsNew { get; set; }
        public bool IsRecomended { get; set; }
        public bool IsDeleted { get; set; }
        public string ImgPath { get; set; }
        public IFormFile MainImage { get; set; }

        public string View { get; set; }

        public List<TechnicalCharacteristic> TechnicalCharacteristics { get; set; } = new List<TechnicalCharacteristic>();

        public List<Product> products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
