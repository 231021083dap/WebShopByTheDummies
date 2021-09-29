using Moq;
using System.Collections.Generic;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests
{
    /*
     * Create_ShouldReturnProductImageResponse (This was removed do to Jack's input regarding images and product correlation)
     */
    public class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        private readonly Mock<IImageRepository> _imageRepository = new Mock<IImageRepository>();
        private readonly Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();

        public ProductServiceTests()
        {
            _sut = new ProductService(_productRepository.Object, _imageRepository.Object, _categoryRepository.Object);
        }

        #region Product

        #region GetAll
        [Fact]
        public async void GetAll_ShouldReturnListOfProductResponses_WhenProductsExist()
        {
            #region Arrange
            List<Product> Products = new List<Product>();

            Products.Add(new Product
            {
                Id = 1,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "test", products = new(), Picture = "test" },
                Name = "test",
                Description = "test",
                Price = 123
            });

            Products.Add(new Product
            {
                Id = 2,
                CategoryId = 2,
                Category = new Category { Id = 2, Name = "test", products = new(), Picture = "test" },
                Name = "test",
                Description = "test",
                Price = 123
            });

            _productRepository
                .Setup(a => a.GetAllProducts())
                .ReturnsAsync(Products);
            #endregion

            #region Act
            var result = await _sut.GetAllProducts();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<ProductResponse>>(result);
            #endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfProductResponses_WhenNoProductsExists()
        {
            #region Arrange
            List<Product> Products = new List<Product>();

            _productRepository
                .Setup(a => a.GetAllProducts())
                .ReturnsAsync(Products);
            #endregion

            #region Act
            var result = await _sut.GetAllProducts();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<ProductResponse>>(result);
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnProductResponse_WhenProductExists()
        {
            #region Arrange
            int productId = 1;

            Product product = new Product
            {
                Id = productId,
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "test", products = new(), Picture = "test" },
                Name = "test",
                Description = "test",
                Price = 123
            };

            _productRepository
                .Setup(a => a.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(product);
            #endregion

            #region Act
            var result = await _sut.GetProductById(productId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.CategoryId, result.Category.Id);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
            #endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            #region Arrange
            int productId = 1;

            _productRepository
                .Setup(a => a.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.GetProductById(productId);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Create
        [Fact]
        public async void Create_ShouldReturnProductResponse_WhenCreateIsSuccess()
        {
            #region Arrange
            int productId = 1;

            NewProduct newProduct = new NewProduct
            {
                Name = "test",
                Description = "test",
                Price = 123,
                CategoryId = 1
            };

            Category category = new Category
            {
                Id = 1,
                Name = "test",
                products = new(),
                Picture = "test"
            };

            Product product = new Product
            {
                Id = productId,
                CategoryId = 1,
                Name = "test",
                Description = "test",
                Price = 123,
                Category = category
            };

            _productRepository
                .Setup(a => a.CreateProduct(It.IsAny<Product>()))
                .ReturnsAsync(product);

            _categoryRepository
                .Setup(a => a.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(category);
            #endregion

            #region Act
            var result = await _sut.CreateProduct(newProduct);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(newProduct.Name, result.Name);
            Assert.Equal(newProduct.Description, result.Description);
            Assert.Equal(newProduct.Price, result.Price);
            Assert.Equal(newProduct.CategoryId, result.Category.Id);
            #endregion
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateProductResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            int productId = 1;

            UpdateProduct updateProduct = new UpdateProduct
            {
                CategoryId = 1,
                Name = "test",
                Description = "test",
                Price = 123
            };

            Product product = new Product
            {
                Id = productId,
                CategoryId = 1,
                Name = "test",
                Description = "test",
                Price = 123,
                Category = new Category { Id = 1, Name = "test", Picture = "test" }
            };

            _productRepository
                .Setup(a => a.UpdateProduct(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(product);
            #endregion

            #region Act
            var result = await _sut.UpdateProduct(productId, updateProduct);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(updateProduct.CategoryId, result.Category.Id);
            Assert.Equal(updateProduct.Name, result.Name);
            Assert.Equal(updateProduct.Description, result.Description);
            Assert.Equal(updateProduct.Price, result.Price);

            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenProductDoesNotExist()
        {
            #region Arrange
            UpdateProduct updateProduct = new UpdateProduct
            {
                CategoryId = 1,
                Name = "test",
                Description = "test",
                Price = 123
            };

            int productId = 1;

            _productRepository
                .Setup(a => a.UpdateProduct(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.UpdateProduct(productId, updateProduct);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Delete
        [Fact]
        public async void DeleteProductImage_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            #region Arrange
            int imageId = 1;

            Image image = new Image
            {
                Id = imageId,
                ProductId = 1,
                Path = "test"
            };

            _imageRepository
                .Setup(a => a.DeleteImage(It.IsAny<int>()))
                .ReturnsAsync(image);
            #endregion

            #region Act
            var result = await _sut.DeleteProduct(imageId);
            #endregion
            
            #region Assert
            Assert.True(result);
            #endregion
        }

        [Fact]
        public async void DeleteProduct_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            #region Arrange
            int productId = 1;

            Image image = new Image
            {
                Id = 1,
                ProductId = productId,
                Path = "test"
            };

            Product product = new Product
            {
                Id = productId,
                Name = "test",
                CategoryId = 1,
                Price = 123,
                Description = "test"
            };

            _productRepository
                .Setup(a => a.DeleteProduct(It.IsAny<int>()))
                .ReturnsAsync(product);
            #endregion

            #region Act
            var result = await _sut.DeleteProduct(productId);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }
        #endregion

        #endregion

        #region Category

        #region GetAll
        [Fact]
        public async void GetAll_ShouldReturnListOfCategoryResponses_WhenCategoriesExist()
        {
            #region Arrange
            List<Category> categories = new List<Category>();
            categories.Add(new Category
            {
                Id = 1,
                Name = "test",
                Picture = "test",
            });

            categories.Add(new Category
            {
                Id = 2,
                Name = "test",
                Picture = "test",
            });

            _categoryRepository
                .Setup(a => a.GetAllCategories())
                .ReturnsAsync(categories);
            #endregion

            #region Act
            var result = await _sut.GetAllCategories();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<CategoryResponse>>(result);
            #endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfCategoryResponses_WhenNoCategoriesExists()
        {
            #region Arrange
            List<Category> categories = new List<Category>();

            _categoryRepository
                .Setup(a => a.GetAllCategories())
                .ReturnsAsync(categories);
            #endregion

            #region Act
            var result = await _sut.GetAllCategories();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<CategoryResponse>>(result);
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnCategoryResponse_WhenCategoryExists()
        {
            #region Arrange
            int categoryId = 1;

            Category category = new Category
            {
                Id = categoryId,
                Name = "test",
                Picture = "test"
            };

            _categoryRepository
                .Setup(a => a.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(category);
            #endregion

            #region Act
            var result = await _sut.GetCategoryById(categoryId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Picture, result.Picture);
            #endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            #region Arrange
            int CategoryId = 1;

            _categoryRepository
                .Setup(a => a.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.GetCategoryById(CategoryId);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Create
        [Fact]
        public async void Create_ShouldReturnCategoryResponse_WhenCreateIsSuccess()
        {
            #region Arrange
            int categoryId = 1;

            NewCategory newCategory = new NewCategory
            {
                Name = "test",
                Picture = "test"
            };

            Category category = new Category
            {
                Id = categoryId,
                Name = "test",
                Picture = "test"
            };

            _categoryRepository
                .Setup(a => a.CreateCategory(It.IsAny<Category>()))
                .ReturnsAsync(category);
            #endregion

            #region Act
            var result = await _sut.CreateCategory(newCategory);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(newCategory.Name, result.Name);
            Assert.Equal(newCategory.Picture, result.Picture);
            #endregion
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateCategoryResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            int categoryId = 1;

            UpdateCategory updateCategory = new UpdateCategory
            {
                Name = "test",
                Picture = "test"
            };

            Category category = new Category
            {
                Id = categoryId,
                Name = "test",
                Picture = "test"
            };

            _categoryRepository
                .Setup(a => a.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>()))
                .ReturnsAsync(category);
            #endregion

            #region Act
            var result = await _sut.UpdateCategory(categoryId, updateCategory);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(updateCategory.Name, result.Name);
            Assert.Equal(updateCategory.Picture, result.Picture);
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            #region Arrange
            UpdateCategory updateCategory = new UpdateCategory
            {
                Name = "test",
                Picture = "test"
            };

            int categoryId = 1;

            _categoryRepository
                .Setup(a => a.UpdateCategory(It.IsAny<int>(), It.IsAny<Category>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.UpdateCategory(categoryId, updateCategory);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Delete
        public async void DeleteCategory_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            #region Arrange
            int categoryId = 1;

            Category category = new Category
            {
                Id = categoryId,
                Name = "test",
                Picture = "test"
            };

            _categoryRepository
                .Setup(a => a.DeleteCategory(It.IsAny<int>()))
                .ReturnsAsync(category);
            #endregion

            #region Act
            var result = await _sut.DeleteCategory(categoryId);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }
        #endregion

        #endregion
    }
}
