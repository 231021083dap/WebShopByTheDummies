using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;

namespace WebShop.API.Services
{
    public class ProductService
    {
        public interface IProductService
        {
            Task<List<ProductResponse>> GetAllProducts();
            Task<ProductResponse> GetProductById(int productId);
            Task<ProductResponse> Create(NewProduct newProduct);
            Task<ProductResponse> Update(int productId, UpdateProduct updateProduct);
            Task<bool> Delete(int productId);
        }
        public class AuthorService : IProductService
        {
            private readonly IProductRepository _productRepository;
            public AuthorService(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<List<ProductResponse>> GetAllProducts()
            {
                List<Product> products = await _productRepository.GetAllProducts();
                return products == null ? null : products.Select(a => new ProductResponse
                {
                    // Id = a.Id,
                    // Name = a.Name,
                    // Price = a.Price,
                    // Description = a.Description
                    // Category = a.CategoryId
                    // Image = a.Images.Select(b => new ProductImageResponse
                    //{ 
                    //  Id = b.Id,
                    //  Path = b.Path
                    // }).ToList()
                }).ToList();
            }

            public Task<ProductResponse> GetProductById(int productId)
            {
                throw new NotImplementedException();
            }


            public Task<ProductResponse> Create(NewProduct newProduct)
            {
                throw new NotImplementedException();
            }

            public Task<bool> Delete(int productId)
            {
                throw new NotImplementedException();
            }

            public Task<ProductResponse> Update(int productId, UpdateProduct updateProduct)
            {
                throw new NotImplementedException();
            }
        }
}
