using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;

namespace WebShop.API.Services
{ 
        public interface IProductService
        {
            Task<List<ProductResponse>> GetAllProducts();
            Task<ProductResponse> GetProductById(int productId);
            Task<ProductResponse> Create(NewProduct newProduct);
            Task<ProductResponse> Update(int productId, UpdateProduct updateProduct);
            Task<bool> Delete(int productId);
        }
        public class ProductService : IProductService
        {
            private readonly IProductRepository _productRepository;
            public ProductService(IProductRepository productRepository)
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

            public async Task<ProductResponse> GetProductById(int productId)
            {
                Product product = await _productRepository.GetProductById(productId);
                return product == null ? null : new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Category = product.CategoryId,
                    Image = product.ImageId
                    //        .Select(b => new ProductImageResponse
                    //{
                    //    Id = b.Id,
                    //    Path = b.Path
                    //}).ToList()
                };
            }


            public async Task<ProductResponse> CreateProduct(NewProduct newProduct)
            {
                Product product = new Product
                {

                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    Description = newProduct.Description,
                    CategoryId = newProduct.CategoryId,
                    ImageId = newProduct.ImageId
                };
            product = await _productRepository.CreateProduct(product);
            return product == null ? null : new ProductResponse
                {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.CategoryId,
                Image = product.ImageId
                };
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
