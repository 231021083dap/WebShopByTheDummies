using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(int productId, Product product);
        Task<Product> DeleteProduct(int productId);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly WebShopContext _context;

        public ProductRepository(WebShopContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product
                //.Include(a => a.CategoryId)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Product
                //.Include(a => a.CategoryId)
                .FirstOrDefaultAsync(a => a.Id == productId);
        }
        public async Task<Product> CreateProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            Product product = await _context.Product.FirstOrDefaultAsync(a => a.Id == productId);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }


        public async Task<Product> UpdateProduct(int productId, Product product)
        {
            Product updateProduct = await _context.Product.FirstOrDefaultAsync(a => a.Id == productId);
            if (updateProduct !=null)
            {
                updateProduct.Name = product.Name;
                updateProduct.Price = product.Price;
                updateProduct.Description = product.Description;
                await _context.SaveChangesAsync();
            }
            return updateProduct;
        }
    }
}
