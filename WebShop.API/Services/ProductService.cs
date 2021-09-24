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
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> GetCategoryById(int categoryId);
        Task<CategoryResponse> CreateCategory(NewCategory newCategory);
        Task<CategoryResponse> UpdateCategory(int categoryId, UpdateCategory updateCategory);
        Task<bool> DeleteCategory(int categoryId);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, IImageRepository imageRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _categoryRepository = categoryRepository;
        }

        #region Get All Products
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
                Images = p.Images.Select(i => new ProductImageResponse
                {
                    Id = i.Id,
                    Path = i.Path,
                    ProductId = p.Id
                }).ToList()
            }).ToList();
        }
        #endregion
        #region Get All Categories
        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<Category> categories = await _categoryRepository.GetAllCategories();
            return categories == null ? null : categories.Select(a => new CategoryResponse
            {
                Id = a.Id,
                Name = a.Name,
                Picture = a.Picture
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
                Images = product.Images.Select(p => new ProductImageResponse
                {
                    Id = p.Id,
                    Path = p.Path,
                    ProductId = product.Id
                }).ToList()
            };
        }
        #endregion
        #region Get Category By Id
        public async Task<CategoryResponse> GetCategoryById(int categoryId)
        {
            Category category = await _categoryRepository.GetCategoryById(categoryId);
            return category == null ? null : new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Picture = category.Picture
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
            if (product != null)
            {
                List<Image> images = new();
                if (newProduct.Image !=null && newProduct.Image.Path.Count > 0)
                {
                    foreach (var item in images)
                    {
                        NewImage newImage = new()
                        {
                            Path = newProduct.Image.Path,
                        };
                        await _imageRepository.CreateImage(item);
                    }
                }
                product.Images = images;
                Category category = await _categoryRepository.GetCategoryById(product.CategoryId);

                return new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Category = new ProductCategoryResponse
                    {
                        Id = category.Id,
                        Name = category.Name

                    },
                    Images = product.Images.Select(a => new ProductImageResponse
                    {
                        Id = a.Id,
                        Path = a.Path
                    }).ToList()
                };
            } return null;


        }
        #endregion
        #region Create Category
        public async Task<CategoryResponse> CreateCategory(NewCategory newCategory)
        {
            Category category = new Category
            {
                Name = newCategory.Name,
                Picture = newCategory.Picture
            };
            category = await _categoryRepository.CreateCategory(category);
            return category == null ? null : new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Picture = category.Picture
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
                    ProductId = product.Id
                };

                image = await _imageRepository.CreateImage(image);

                return image == null ? null : new ProductImageResponse
                {
                    Id = image.Id,
                    Path = image.Path,
                    ProductId = image.ProductId
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
        #region Delete Category
        public async Task<bool> DeleteCategory(int categoryId)
        {
            var result = await _categoryRepository.DeleteCategory(categoryId);
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
                Description = product.Description,
                Category = new ProductCategoryResponse
                {
                    Id = product.CategoryId,
                    Name = product.Category.Name
                }

            };

        }
        #endregion
        #region Update Category
        public async Task<CategoryResponse> UpdateCategory(int categoryId, UpdateCategory updateCategory)
        {
            Category category = new Category
            {
                Name = updateCategory.Name,
                Picture = updateCategory.Picture
            };
            category = await _categoryRepository.UpdateCategory(categoryId, category);

            return category == null ? null : new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Picture = category.Picture
            };
        }
        #endregion
    }
}
