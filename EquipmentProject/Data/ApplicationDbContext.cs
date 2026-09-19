using EquipmentProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EquipmentProject.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<TechnicalCharacteristic> TechnicalCharacteristics { get; set; }
        public DbSet<WhyWe> WhyWe { get; set; }

        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity(typeof(IdentityPasskeyData)).HasNoKey();

            builder.Entity<SiteSettings>().HasData(
                new SiteSettings { Id = 1 }
            );
            builder.Entity<Contact>().HasData(
                new Contact { Id = 1, SiteSettingsId = 1 }
            );

            
        }

    }
}

