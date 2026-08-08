namespace EquipmentProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Articul {  get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId {  get; set; }
        public string ShortDescription { get; set; }
        public  string FullDescription { get; set; }
        public bool IsNew { get; set; }
        public bool IsRecomended { get; set; }
        public bool IsDeleted { get; set; }
        public string ImgPath {  get; set; }
        public Subcategory Subcategory { get; set; }
        public List<TechnicalCharacteristic>TechnicalCharacteristics { get; set; }

        

       



    }
}
