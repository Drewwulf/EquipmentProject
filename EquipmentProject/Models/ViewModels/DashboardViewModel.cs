using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentProject.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int SubcategoriesCount { get; set; }
        public int ProductsCount { get; set; }
    }
}
