﻿using System;
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
        Task<ProductResponse> UpdateProduct(int productId, UpdateProduct updateProduct);
        Task<bool> DeleteProduct(int productId);
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #region Get Áll Products
        public async Task<List<ProductResponse>> GetAllProducts()
        {
            List<Product> products = await _productRepository.GetAllProducts();
            return products == null ? null : products.Select(a => new ProductResponse
            {
                Id = a.Id,
                Name = a.Name,
                Price = a.Price,
                Description = a.Description,
                Category = new ProductCategoryResponse
                {
                    Id = a.Category.Id,
                    Name = a.Category.Name
                },
                Images = a.Image.Select(b => new ImageResponse
                {
                    Id = b.Id,
                    Path = b.Path
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
                Images = product.Image.Select(b => new ImageResponse
                {
                    Id = b.Id,
                    Path = b.Path
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
                Images = product.Image.Select(a => new ImageResponse
                {
                    Id = a.Id,
                    Path = a.Path
                }).ToList()
            };




        }
        #endregion
        #region Delete Product
        public async Task<bool> DeleteProduct(int productId)
        {
            var result = await _productRepository.DeleteProduct(productId);
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
