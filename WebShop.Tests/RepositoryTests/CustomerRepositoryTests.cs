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
    public class CustomerRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly CustomerRepository _sut;

        public CustomerRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShoptest")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new CustomerRepository(_context);
        }
        [Fact]
        public async Task GetAllCustomers_ShouldReturnAListOfCustomers_WhenCustomersExists()
        {
        #region Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Customer.Add(new Customer
            {
                Id = 1,
                FirstName = "Testen",
                MiddleName = "Test",
                LastName = "Testesen",
                User = new User { Id = 1, Email = "Test@Test.test"},
                UserId = 1
            });
            _context.Customer.Add(new Customer
            {
                Id = 2,
                FirstName = "Testen2",
                MiddleName = "Test2",
                LastName = "Testesen2",
                User = new User { Id = 2, Email = "Test2@Test.test" },
                UserId = 2
            });
            await _context.SaveChangesAsync();
        #endregion
        #region Act
        var result = await _sut.GetAllCustomers();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Customer>>(result);
            Assert.Equal(2, result.Count);
            #endregion
        }
}




    //[Fact]
    //public async Task
    //{
    #region Arrange
    //await _context.Database.EnsureDeletedAsync();
    #endregion
    #region Act
    //var result = await _sut.UpdateAddress(addressId, updateAddress);
    #endregion
    #region Assert
    #endregion
    //}
}
