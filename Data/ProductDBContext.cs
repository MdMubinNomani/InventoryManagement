using InventoryManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagement.Data
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions <ProductDBContext> option) : base(option)
        {
            
        }

        public DbSet<Product> product { get; set; }
    }
}
