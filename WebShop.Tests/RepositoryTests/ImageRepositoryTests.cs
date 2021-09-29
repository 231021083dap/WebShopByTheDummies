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
    public class ImageRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly ImageRepository _sut;


        public ImageRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTest")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new ImageRepository(_context);
        }
        [Fact]
        public async Task GetAllImages_ShouldReturnListOfImages_WhenImagesExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Image.Add(new Image
            {
                Id = 1,
                Path = "Test/Path",
                Product = new() { Id = 1},
                ProductId = 1
            });
            _context.Image.Add(new Image
            {
                Id = 2,
                Path = "Test/Path",
                ProductId = 1
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetAllImages();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Image>>(result);
            Assert.Equal(2, result.Count);
            #endregion
        }
        [Fact]
        public async Task GetAllImages_ShouldReturnEmptyListOfImages_WhenNoImagesExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            #endregion
            #region Act
            var result = await _sut.GetAllImages();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Image>>(result);
            Assert.Empty(result);
            #endregion
        }

        [Fact]
        public async Task GetImageById_ShouldReturnTheImage_IfImageExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int imageId = 1;
            _context.Image.Add(new Image
            {
                Id = imageId,
                Path = "Test/Path",
                Product = new() { Id = 1 },
                ProductId = 1
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetImageById(imageId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Image>(result);
            Assert.Equal(imageId, result.Id);
            #endregion
        }
        [Fact]
        public async Task GetImageById_ShouldReturnNull_IfImageDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int imageId = 1;
            #endregion
            #region Act
            var result = await _sut.GetImageById(imageId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        [Fact]
        public async Task CreateImage_ShouldAddIdToImage_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Image image = new()
            {
                Path = "Test/Path",
                Product = new() { Id = 1 },
                ProductId = 1
            };
            #endregion
            #region Act
            var result = await _sut.CreateImage(image);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Image>(result);
            Assert.Equal(expectedId, result.Id);
            #endregion
        }
        [Fact]
        public async Task CreateImage_ShouldFailToAddNewImage_WhenAddingImageWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            Image image = new()
            {
                Id = 1,
                Path = "Test/Path",
                Product = new() { Id = 1 },
                ProductId = 1
            };

            _context.Image.Add(image);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateImage(image);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }
        
        [Fact]
        public async Task DeleteImage_ShouldReturnImageCategory_WhenImageIsDeleted()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int imageId = 1;
            Image image = new()
            {
                Id = imageId,
                Path = "Test/Path",
                Product = new() { Id = 1 },
                ProductId = 1

            };
            _context.Image.Add(image);
            await _context.SaveChangesAsync();

            #endregion
            #region Act
            var result = await _sut.DeleteImage(imageId);
            var deletedImage = await _sut.GetAllImages();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Image>(result);
            Assert.Equal(imageId, result.Id);

            Assert.Empty(deletedImage);
            #endregion


        }

        [Fact]
        public async Task DeleteImage_ShouldReturnNull_WhenImageDoesNotExist()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int imageId = 1;
            #endregion
            #region Act
            var result = await _sut.DeleteImage(imageId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
    }
}
