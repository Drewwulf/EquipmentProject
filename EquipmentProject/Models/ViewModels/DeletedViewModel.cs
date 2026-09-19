using System.Collections.Generic;

namespace EquipmentProject.Models.ViewModels
{
    public class DeletedDataViewModel
    {
        public List<Category> Categories { get; set; } = new();

        public List<Subcategory> Subcategories { get; set; } = new();

        public List<Product> Products { get; set; } = new();
    }
}