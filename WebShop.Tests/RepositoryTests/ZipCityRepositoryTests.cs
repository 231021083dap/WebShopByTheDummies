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
    public class ZipCityRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly ZipCityRepository _sut;


        public ZipCityRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTest")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new ZipCityRepository(_context);
        }
        [Fact]
        public async Task GetZipCityyById_ShouldReturnTheZipCity_IfZipCityExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int zipCityId = 1;
            _context.ZipCity.Add(new ZipCity
            {
                Id = zipCityId,
                City = "Andeby",
                
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetZipCityById(zipCityId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<ZipCity>(result);
            Assert.Equal(zipCityId, result.Id);
            #endregion
        }
        [Fact]
        public async Task GetZipCityById_ShouldReturnNull_IfCityDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int zipCityId = 1;
            #endregion
            #region Act
            var result = await _sut.GetZipCityById(zipCityId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
    }
}
