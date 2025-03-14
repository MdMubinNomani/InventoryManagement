using InventoryManagement.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIDAsync(int id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(Product product);
        bool ProductExists(int PId);
    }
}
