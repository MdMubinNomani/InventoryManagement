using InventoryManagement.Data;
using InventoryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _context;

        public ProductRepository(ProductDBContext context)
        {
            _context = context;
        }

        public void Delete(Product product)
        {
            _context.Remove(product);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.product.ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIDAsync(Guid? id)
        {
            var product = await _context.product.FirstOrDefaultAsync(m => m.PId == id);
            return product;
        }

        public void Insert(Product product)
        {
            product.PId = Guid.NewGuid();
            _context.Add(product);
        }

        public void Update(Product product)
        {
            _context.product.Update(product);
        }

        public bool ProductExists(Guid id)
        {
            return _context.product.Any(e => e.PId == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.product;
        }
    }
}
