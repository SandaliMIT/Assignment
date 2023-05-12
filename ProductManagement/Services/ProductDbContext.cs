using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Services
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LP-HQ-PF2PNK5A\SQLEXPRESS; Initial Catalog=InventoryDb; TrustServerCertificate= True; Integrated Security=True");
        }
    }
}
