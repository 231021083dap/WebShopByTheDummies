using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;
using Xunit;

namespace WebShop.Tests.RepositoryTests
{
    public class CategoryRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly CategoryRepository _sut;

        public CategoryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTestCategory")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new CategoryRepository(_context);
        }

        #region GetAll
        [Fact]
        public async Task GetAllCategories_ShouldReturnListOfCategories_WhenCategoriesExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Category.Add(new Category
            {
                Id = 1,
                Name = "TestCategory",
                Picture = "TestPicturePath"
            });
            _context.Category.Add(new Category
            {
                Id = 2,
                Name = "TestCategory2",
                Picture = "TestPicturePath2"
            });
            await _context.SaveChangesAsync();
        #endregion
            #region Act
        var result = await _sut.GetAllCategories();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
        #endregion
        }
        [Fact]
        public async Task GetAllCategories_ShouldReturnEmptyListOfCategories_WhenNoCategoriesExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            
            #endregion
            #region Act
            var result = await _sut.GetAllCategories();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Empty(result);
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetCategoryById_ShouldReturnTheCategory_IfCategoryExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            _context.Category.Add(new Category
            {
                Id = categoryId,
                Name = "TestCategory",
                Picture = "TestPath"
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetCategoryById(categoryId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            #endregion
        }
        [Fact]
        public async Task GetCategoryById_ShouldReturnNull_IfCategoryDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            #endregion
            #region Act
            var result = await _sut.GetCategoryById(categoryId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Create
        [Fact]
        public async Task CreateCategory_ShouldAddIdToCategory_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Category category = new()
            {
                Name = "TestCategory",
                Picture = "testCategoryPicture"
            };
            #endregion
            #region Act
            var result = await _sut.CreateCategory(category);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(expectedId, result.Id);
            #endregion
        }
        [Fact]
        public async Task CreateCategory_ShouldFailToAddNewCategory_WhenAddingCategoryWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            Category category = new()
            {
                Id = 1,
                Name = "TestCategory",
                Picture = "TestCategoryPicture"
            };

            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateCategory(category);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }
        #endregion

        #region Update
        [Fact]
        public async Task UpdateCategory_ShouldChangeValuesOnCategory_WhenCategoryExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            Category category = new()
            {
                Id = categoryId,
                Name = "testCategory",
                Picture = "testCategoryPic"
            };
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            Category updateCategory = new()
            {
                Id = categoryId,
                Name = "Test2Category",
                Picture = "Test2CategoryPicture"
            };
            #endregion
            #region Act
            var result = await _sut.UpdateCategory(categoryId, updateCategory);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal(updateCategory.Name, result.Name);
            Assert.Equal(updateCategory.Picture, result.Picture);
         
            #endregion
        }
        [Fact]
        public async Task UpdateCategory_ShouldReturnNull_WhenCategoryDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            Category updateCategory = new()
            {
                Id = categoryId,
                Name = "TestCategory",
                Picture = "TestCategoryPicture"
            };
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
        [Fact]
        public async Task DeleteCategory_ShouldReturnDeletedCategory_WhenCategoryIsDeleted()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            Category category = new()
            {
                Id = categoryId,
                Name = "CategoryTest",
                Picture = "TestPathPicture"

            };
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            #endregion
            #region Act
            var result = await _sut.DeleteCategory(categoryId);
            var deletedAddress = await _sut.GetAllCategories();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);

            Assert.Empty(deletedAddress);
            #endregion
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int categoryId = 1;
            #endregion
            #region Act
            var result = await _sut.DeleteCategory(categoryId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion
    }
}
