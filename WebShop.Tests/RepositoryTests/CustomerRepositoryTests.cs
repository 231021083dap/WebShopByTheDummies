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

        #region GetAll
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
                User = new User { Id = 1, Email = "Test@Test.test" },
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
        [Fact]
        public async Task GetAllCustomers_ShouldReturnEmptyListOfCustomers_WhenNoCustomersExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            #endregion
            #region Act
            var result = await _sut.GetAllCustomers();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<Customer>>(result);
            Assert.Empty(result);
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetCustomerById_ShouldReturnTheCustomer_IfCustomerExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int customerId = 1;
            _context.Customer.Add(new Customer
            {
                Id = customerId,
                FirstName = "Testen",
                MiddleName = "Test",
                LastName = "Testesen",
                User = new User { Id = 1, Email = "Test@Test.test" },
                UserId = 1

            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetCustomerById(customerId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(customerId, result.Id);
            #endregion
        }
        [Fact]
        public async Task GetCustomerById_ShouldReturnNull_IfCustomerDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int customerId = 1;
            #endregion
            #region Act
            var result = await _sut.GetCustomerById(customerId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Create
        [Fact]
        public async Task CreateCustomer_ShouldAddIdToCustomer_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Customer customer = new()
            {
                FirstName = "Testen",
                MiddleName = "Test",
                LastName = "Testesen",
                User = new User { Id = 1, Email = "Test@Test.test" },
                UserId = 1
            };
            #endregion
            #region Act
            var result = await _sut.CreateCustomer(customer);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(expectedId, result.Id);
            #endregion
        }
        [Fact]
        public async Task CreateCustomer_ShouldFailToAddNewCustomer_WhenAddingCustomerWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            Customer customer = new()
            {
                Id = 1,
                FirstName = "Testen",
                MiddleName = "Test",
                LastName = "Testesen",
                User = new User { Id = 1, Email = "Test@Test.test" },
                UserId = 1
            };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateCustomer(customer);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }
        #endregion

        #region update
        [Fact]
        public async Task UpdateCustomer_ShouldChangeValuesOnCustomer_WhenCustomerExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int customerId = 1;
            Customer customer = new()
            {
                Id = customerId,
                FirstName = "Tester",
                MiddleName = "Test",
                LastName = "Testesen",
                User = new User { Id = 1, Email = "Test@Test.test" },
                UserId = 1

            };
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            Customer updateCustomer = new()
            {
                Id = customerId,
                FirstName = "Tester2",
                MiddleName = "Test12",
                LastName = "Testesen2",
                User = new User { Id = 1, Email = "Test@Test.test" },
                UserId = 1
            };
            #endregion
            #region Act
            var result = await _sut.UpdateCustomer(customerId, updateCustomer);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Customer>(result);
            Assert.Equal(customerId, result.Id);
            Assert.Equal(updateCustomer.FirstName, result.FirstName);
            Assert.Equal(updateCustomer.MiddleName, result.MiddleName);
            Assert.Equal(updateCustomer.LastName, result.LastName);
            Assert.Equal(updateCustomer.UserId, result.UserId);

            #endregion
        }
        [Fact]
        public async Task UpdateCustomer_ShouldReturnNull_WhenCustomerDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int customerId = 1;
            Customer updateCustomer = new()
            {
                Id = customerId,
                FirstName = "Tester2",
                MiddleName = "Test12",
                LastName = "Testesen2",
                User = new User { Id = 1, Email = "Test@Test.test" },
                UserId = 1
            };
            #endregion
            #region Act
            var result = await _sut.UpdateCustomer(customerId, updateCustomer);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion
    }
}
