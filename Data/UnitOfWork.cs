using InventoryManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductDBContext _context;
        private IProductRepository? _productRepository; // Nullable field

        public UnitOfWork(ProductDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ??= new ProductRepository(_context);
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
