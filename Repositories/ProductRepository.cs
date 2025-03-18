﻿using InventoryManagement.Data;
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
            //var products = await _context.product.ToListAsync(); // Direct Fetch
            //return products;

            var products = await _context.product.FromSqlRaw("GetAllProducts").ToListAsync(); // Stored Procedure Fetch
            return products;
        }

        public async Task<Product?> GetByIDAsync(int id)
        {
            //var product = await _context.product.FirstOrDefaultAsync(m => m.Id == id); // Direct Fetch
            //return product;

            var product = await _context.product.FromSqlInterpolated($"EXEC GetById {id}").ToListAsync(); // Stored Procedure Fetch
            return product.FirstOrDefault(); // Converting List to Product model instance
        }

        public void Insert(Product product)
        {
            _context.Add(product);
        }

        public void Update(Product product)
        {
            _context.product.Update(product);
        }

        public bool ProductExists(int id)
        {
            return _context.product.Any(e => e.Id == id); // Direct Fetch
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.product.FromSqlRaw("GetAllProducts"); // Stored Procedure Fetch
        }
    }
}
