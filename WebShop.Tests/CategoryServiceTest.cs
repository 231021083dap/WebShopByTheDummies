using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Responses;
using WebShop.API.Services;
using Xunit;
using static WebShop.API.Repository.CategoryRepository;

namespace WebShop.Tests
{
    public class CategoryServiceTest
    {
        private readonly CategoryService _sut;
        private readonly Mock<ICategoryRepository> _categoryRepository = new();

        public CategoryServiceTest()
        {
            _sut = new CategoryService(_categoryRepository.Object);
        }

        [Fact]
        public async void GetAllCategories_ShouldReturnListOfCategories()
        {
            #region Arrange
            List<Category> categories = new List<Category>();
            categories.Add(new Category
            {
                Id = 1,
                Name = "Sport",
                Picture = "/Sti/til/billede"
            });
            categories.Add(new Category
            {
                Id = 2,
                Name = "Fest",
                Picture = "/Sti/til/billede2"

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
    }

}

