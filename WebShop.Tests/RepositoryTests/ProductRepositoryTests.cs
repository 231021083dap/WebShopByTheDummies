using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;
using Xunit;

namespace WebShop.Tests.RepositoryTests
{
    public class ProductRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly ProductRepository _sut;


        public ProductRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTest")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new ProductRepository(_context);
        }
        [Fact]
        public async Task GetAllProducts_ShouldReturnListOfProducts_WhenProductsExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Product.Add(new Product
            {
                Id = 1,
                Name = "TestProduct",
                Description = "Testproduct",
                Price = 1337,
                Category = new Category { Id = 1 },
                CategoryId = 1,
            });
            _context.Product.Add(new Product
            {
                Id = 2,
                Name = "TestProduct21",
                Description = "Testproduct21",
                Price = 1337,
                CategoryId = 1,
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetAllProducts();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count);
            #endregion
        }
        [Fact]
        public async Task GetAllProducts_ShouldReturnEmptyListOfProducts_WhenNoProductsExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            #endregion
            #region Act
            var result = await _sut.GetAllProducts();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);
            #endregion
        }

        [Fact]
        public async Task GetProductById_ShouldReturnTheProduct_IfProductExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            _context.Product.Add(new Product
            {
                Id = productId,
                Name = "TestProduct",
                Description = "Testproduct",
                Price = 1337,
                Category = new Category { Id = 1 },
                CategoryId = 1,
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetProductById(productId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            #endregion
        }
        [Fact]
        public async Task GetProductById_ShouldReturnNull_IfProductDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            #endregion
            #region Act
            var result = await _sut.GetProductById(productId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        [Fact]
        public async Task CreateProduct_ShouldAddIdToProduct_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Product product = new()
            {
                Name = "TestProduct",
                Description = "Testproduct",
                Price = 1337,
                Category = new Category { Id = 1 },
                CategoryId = 1,
            };
            #endregion
            #region Act
            var result = await _sut.CreateProduct(product);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(expectedId, result.Id);
            #endregion
        }
        [Fact]
        public async Task CreateProduct_ShouldFailToAddNewProduct_WhenAddingProductWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            Product product = new()
            {
                Id = 1,
                Name = "TestProduct",
                Description = "Testproduct",
                Price = 1337,
                Category = new Category { Id = 1 },
                CategoryId = 1,
            };

            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateProduct(product);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }
        [Fact]
        public async Task UpdateProduct_ShouldChangeValuesOnProduct_WhenProductExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product product = new()
            {
                Id = productId,
                Name = "TestProduct",
                Description = "Testproduct",
                Price = 1337,
                Category = new Category { Id = 1 },
                CategoryId = 1,
            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            Product updateProduct = new()
            {
                Id = productId,
                Name = "TestProduct2",
                Description = "Testproduct2",
                Price = 1337,
                CategoryId = 1
            };
            #endregion
            #region Act
            var result = await _sut.UpdateProduct(productId, updateProduct);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(updateProduct.Name, result.Name);
           // Assert.Equal(updateCategory.Picture, result.Picture);

            #endregion
        }
        [Fact]
        public async Task UpdateProduct_ShouldReturnNull_WhenProductDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product updateProduct = new()
            {
                Id = productId,
                Name = "TestProduct",
                Description = "Testproduct",
                Price = 1337,
                Category = new Category { Id = 1},
                CategoryId = 1,
                                
            };
            #endregion
            #region Act
            var result = await _sut.UpdateProduct(productId, updateProduct);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion

        }
        [Fact]
        public async Task DeleteProduct_ShouldReturnDeletedProduct_WhenProductIsDeleted()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            Product product = new()
            {
                //Id = categoryId,
                //Name = "CategoryTest",
                //Picture = "TestPathPicture"

            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            #endregion
            #region Act
            var result = await _sut.DeleteProduct(productId);
            var deletedProduct = await _sut.GetAllProducts();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);

            Assert.Empty(deletedProduct);
            #endregion


        }

        [Fact]
        public async Task DeleteProduct_ShouldReturnNull_WhenProductDoesNotExist()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int productId = 1;
            #endregion
            #region Act
            var result = await _sut.DeleteProduct(productId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
    }
}
