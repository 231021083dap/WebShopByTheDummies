using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests
{
    public class ProductServiceTest
    {
        private readonly ProductService _sut;
        private readonly Mock<IProductRepository> _productRepository = new();
        public ProductServiceTest()
        {
            _sut = new ProductService(_productRepository.Object);
        }
        [Fact]
        public async void GetAllProducts_ShouldReturnListOfProductResponses_WhenProductExist()
        {

            #region Arrange
            List<Product> products = new List<Product>();
            products.Add(new Product
            {
                Id = 1,
                Name = "Lej en ven",
                CategoryId = 2,
                Price = 1000,
                Description = "Lej en pseudo ven for en hel formiddag/eftermiddag/aften",
                Category = new Category
                {
                    Id = 2,
                    Name = "One",
                    Picture = "two"
                }
                
            });
            products.Add(new Product
            {
                Id = 2,
                Name = "Lej en baneløber",
                CategoryId = 2,
                Price = 4200,
                Description = "Lej person til at iterefere med en sportskamp",
                Category = new Category
                {
                    Id = 2,
                    Name = "One",
                    Picture = "two"
                }


            });
            _productRepository
                .Setup(a => a.GetAllProducts())
                .ReturnsAsync(products);

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
    }
}
