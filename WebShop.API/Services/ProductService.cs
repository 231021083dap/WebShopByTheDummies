using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;

namespace WebShop.API.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(int productId);
        Task<ProductResponse> CreateProduct(NewProduct newProduct);
        Task<ProductImageResponse> CreateProductImage(NewProductImage newProductImage, int productId);
        Task<bool> DeleteProductImage(int imageId);
        Task<ProductResponse> UpdateProduct(int productId, UpdateProduct updateProduct);
        Task<bool> DeleteProduct(int productId);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        public ProductService(IProductRepository productRepository, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
        }
        #region Get Áll Products
        public async Task<List<ProductResponse>> GetAllProducts()
        {
            List<Product> products = await _productRepository.GetAllProducts();
            return products == null ? null : products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = new ProductCategoryResponse
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name
                },
                Images = p.Image.Select(i => new ProductImageResponse
                {
                    imageId = i.Id,
                    Path = i.Path,
                    productId = p.Id
                }).ToList()
            }).ToList();
        }
        #endregion
        #region Get Product By Id
        public async Task<ProductResponse> GetProductById(int productId)
        {
            Product product = await _productRepository.GetProductById(productId);
            return product == null ? null : new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = new ProductCategoryResponse
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                },
                Images = product.Image.Select(p => new ProductImageResponse
                {
                    imageId = p.Id,
                    Path = p.Path,
                    productId = product.Id
                }).ToList()
            };
        }
        #endregion
        #region Create Product
        public async Task<ProductResponse> CreateProduct(NewProduct newProduct)
        {
            Product product = new Product
            {

                Name = newProduct.Name,
                Price = newProduct.Price,
                Description = newProduct.Description,
                CategoryId = newProduct.CategoryId,

            };
            product = await _productRepository.CreateProduct(product);
            return product == null ? null : new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = new ProductCategoryResponse
                {
                    Id = product.Category.Id
                },
                Images = product.Image.Select(a => new ProductImageResponse
                {
                    imageId = a.Id,
                    productId = product.Id,
                    Path = a.Path
                }).ToList()
            };
        }
        #endregion
        #region Create Image
        public async Task<ProductImageResponse> CreateProductImage(NewProductImage newProductImage, int productId)
        {

            Product product = await _productRepository.GetProductById(productId);
            if (product != null)
            {
                Image image = new Image
                {
                    Path = newProductImage.Path,
                    productId = product.Id
                };

                image = await _imageRepository.CreateImage(image);

                return image == null ? null : new ProductImageResponse
                {
                    imageId = image.Id,
                    Path = image.Path,
                    productId = image.productId
                };
            }
            return null;

        }
        #endregion
        #region Delete Product
        public async Task<bool> DeleteProduct(int productId)
        {
            var result = await _productRepository.DeleteProduct(productId);
            return true;
        }
        #endregion
        #region Delete Image
        public async Task<bool> DeleteProductImage(int imageId)
        {
            var result = await _imageRepository.DeleteImage(imageId);
            return true;
        }
        #endregion
        
        #region Update Product
        public async Task<ProductResponse> UpdateProduct(int productId, UpdateProduct updateProduct)
        {
            Product product = new Product
            {
                Name = updateProduct.Name,
                Price = updateProduct.Price,
                Description = updateProduct.Description,
                CategoryId = updateProduct.CategoryId,

            };
            product = await _productRepository.UpdateProduct(productId, product);
            return product == null ? null : new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description

            };

        }
        #endregion
    }

}
