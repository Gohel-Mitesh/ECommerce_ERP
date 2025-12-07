using ECommerce_ERP.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_ERP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Category> CategoryMaster { get; set; }
        public DbSet<Product> ProductsMaster { get; set; }
        public DbSet<ProductVariant> ProductVariantsDetails { get; set; }
        public DbSet<ProductVariantPhoto> ProductVariantPhotos { get; set; }

        public DbSet<User> UserMaster { get; set; }
    }
}
