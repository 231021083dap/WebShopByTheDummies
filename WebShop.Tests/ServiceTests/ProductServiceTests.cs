using Moq;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests
{
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
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfProductResponses_WhenNoProductsExists()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnProductResponse_WhenProductExists()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region Create
        [Fact]
        public async void Create_ShouldReturnProductResponse_WhenCreateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void Create_ShouldReturnProductImageResponse_WhenCreateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateProductResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenProductDoesNotExist()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
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
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfCategoryResponses_WhenNoCategoriesExists()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnCategoryResponse_WhenCategoryExists()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region Create
        [Fact]
        public async void Create_ShouldReturnCategoryResponse_WhenCreateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateCategoryResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
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
