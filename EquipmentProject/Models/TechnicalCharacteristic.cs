namespace EquipmentProject.Models
{
    public class TechnicalCharacteristic
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ParamentName { get; set; }
        public string ParamentValue { get; set; }

        public Product Product { get; set; }
    }
}
