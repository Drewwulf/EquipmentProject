namespace EquipmentProject.Models.ViewModels
{
    public class ProductViewModel
    {
       
        public string ProductName { get; set; }
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

        public List<TechnicalCharacteristic> TechnicalCharacteristics { get; set; } = new List<TechnicalCharacteristic>();


        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
