//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebShop.API.Database.Entities;
//using WebShop.API.DTO.Responses;
//using WebShop.API.Repository;
//using WebShop.API.Services;
//using Xunit;

//namespace WebShop.Tests
//{
//    public class ProductServiceTest
//    {
//        private readonly ProductService _sut;
//        private readonly Mock<IProductRepository> _productRepository = new();
//        private readonly Mock<IImageRepository> _imageRepository = new();
//        public ProductServiceTest()
//        {
//            _sut = new ProductService(_productRepository.Object, _imageRepository.Object);
//        }
//        [Fact]
//        public async void GetAllProducts_ShouldReturnListOfProductResponses_WhenProductExist()
//        {

//            #region Arrange
//            List<Product> products = new List<Product>();
//            products.Add(new Product
//            {
//                Id = 1,
//                Name = "Lej en ven",
//                CategoryId = 2,
//                Price = 1000,
//                Description = "Lej en pseudo ven for en hel formiddag/eftermiddag/aften",
//                Category = new Category
//                {
//                    Id = 2,
//                    Name = "One",
//                    Picture = "two"
//                }

//            });
//            products.Add(new Product
//            {
//                Id = 2,
//                Name = "Lej en baneløber",
//                CategoryId = 2,
//                Price = 4200,
//                Description = "Lej person til at iterefere med en sportskamp",
//                Category = new Category
//                {
//                    Id = 2,
//                    Name = "One",
//                    Picture = "two"
//                }


//            });
//            _productRepository
//                .Setup(p => p.GetAllProducts())
//                .ReturnsAsync(products);

//            #endregion

//            #region Act
//            var result = await _sut.GetAllProducts();
//            #endregion

//            #region Assert
//            Assert.NotNull(result);
//            Assert.Equal(2, result.Count);
//            Assert.IsType<List<ProductResponse>>(result);
//            #endregion

//        }
//        [Fact]
//        public async void GetAllProducts_ShouldReturnEmptyListOfProductResponses_WhenNoProductsExists()
//        {
//            #region Arrange
//            List<Product> products = new List<Product>();

//            _productRepository
//                .Setup(p => p.GetAllProducts())
//                .ReturnsAsync(products);
//            #endregion

//            #region Act
//            var result = await _sut.GetAllProducts();
//            #endregion

//            #region Assert
//            Assert.NotNull(result);
//            Assert.Empty(result);
//            Assert.IsType<List<ProductResponse>>(result);
//            #endregion

//        }
//        [Fact]
//        public async void GetProductById_ShouldReturnAProductResponse_WhenProductExists()
//        {

//            #region Arrange
//            int productId = 1;

//            Product product = new Product
//            {
//                Id = productId,
//                Name = "Streaker",
//                CategoryId = 1,
//                Description = "Nøgen mand/kvinde der løber",
//                Price = 1850,
//                Category = new Category
//                {
//                    Id = 1,
//                    Name = "Sport",
//                    Picture = "/dadf/da"
//                }
//            };

//            _productRepository
//                .Setup(p => p.GetProductById(It.IsAny<int>()))
//                .ReturnsAsync(product);
//            #endregion

//            #region Act
//            var result = await _sut.GetProductById(productId);
//            #endregion

//            #region Assert
//            Assert.NotNull(result);
//            Assert.IsType<ProductResponse>(result);
//            Assert.Equal(product.Id, result.Id);
//            Assert.Equal(product.Name, result.Name);
//            Assert.Equal(product.CategoryId, result.Category.Id);
//            Assert.Equal(product.Description, result.Description);
//            Assert.Equal(product.Price, result.Price);

//            #endregion

//        }
//    }
//}
