using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests.RepositoryTests
{
    public class AddressRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly AddressRepository _sut;


        public AddressRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTest")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new AddressRepository(_context);
        }

        [Fact]
        public async Task GetAddresById_ShouldReturnTheAddress_IfAddressExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int addressId = 3;
            _context.Address.Add(new Address
            {
                Id = addressId,
                StreetName = "TestStreet",
                Number = 12,
                Floor = "2th",
                ZipCity = new ZipCity { Id = 2400, City = "TestBronx" },
                ZipCityId = 2400,
                Country = "Danmark",
                Customer = new Customer { Id = 2 },
                CustomerId = 2
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetAddressById(addressId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Address>(result);
            Assert.Equal(addressId, result.Id);
            #endregion
        }

        [Fact]
        public async Task GetAddressById_ShouldReturnNull_IfAddressDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int addressId = 1;
            #endregion
            #region Act
            var result = await _sut.GetAddressById(addressId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        [Fact]
        public async Task CreateAddress_ShouldAddIdToAddress_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Address address = new()
            {
                StreetName = "TestStreet",
                Number = 12,
                Floor = "2th",
                ZipCity = new ZipCity { Id = 2400, City = "TestBronx" },
                ZipCityId = 2400,
                Country = "Danmark",
                Customer = new Customer { Id = 2 },
                CustomerId = 2
            };
            #endregion
            #region Act
            var result = await _sut.CreateAddress(address);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Address>(result);
            Assert.Equal(expectedId, result.Id);
            #endregion
        }
        [Fact]
        public async Task CreateAddress_ShouldFailToAddNewAddress_WhenAddingAddressWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            Address address = new()
            {
                Id = 1,
                StreetName = "TestStreet",
                Number = 12,
                Floor = "2th",
                ZipCity = new ZipCity { Id = 2400, City = "TestBronx" },
                ZipCityId = 2400,
                Country = "Danmark",
                Customer = new Customer { Id = 2 },
                CustomerId = 2
            };

            _context.Address.Add(address);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateAddress(address);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }
        [Fact]
        public async Task UpdateAddress_ShouldChangeValuesOnAddress_WhenAddressExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int addressId = 1;
            Address address = new()
            {
                Id = addressId,
                StreetName = "TestStreet",
                Number = 12,
                Floor = "2th",
                ZipCity = new ZipCity { Id = 2400, City = "TestBronx" },
                ZipCityId = 2400,
                Country = "Danmark",
                Customer = new Customer { Id = 2 },
                CustomerId = 2
            };
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            Address updateAddress = new()
            {
                Id = addressId,
                StreetName = "UpdatedStreet",
                Number = 21,
                Floor = "update 3th",
                ZipCity = new ZipCity { Id = 4200, City = "TestBronx" },
                ZipCityId = 4200,
                Country = "Update",
                Customer = new Customer { Id = 2 },
                CustomerId = 2
            };
            #endregion
            #region Act
            var result = await _sut.UpdateAddress(addressId, updateAddress);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Address>(result);
            Assert.Equal(addressId, result.Id);
            Assert.Equal(updateAddress.StreetName, result.StreetName);
            Assert.Equal(updateAddress.Number, result.Number);
            Assert.Equal(updateAddress.Floor, result.Floor);
            Assert.Equal(updateAddress.CustomerId, result.CustomerId);
            Assert.Equal(updateAddress.ZipCityId, result.ZipCityId);
            Assert.Equal(updateAddress.Country, result.Country);
            #endregion
        }
        [Fact]
        public async Task UpdateAddress_ShouldReturnNull_WhenAddressDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int addressId = 1;
            Address updateAddress = new()
            {
                Id = addressId,
                StreetName = "TestStreet",
                Number = 12,
                Floor = "2th",
                ZipCity = new ZipCity { Id = 2400, City = "TestBronx" },
                ZipCityId = 2400,
                Country = "Danmark",
                Customer = new Customer { Id = 2 },
                CustomerId = 2
            };
            #endregion
            #region Act
            var result = await _sut.UpdateAddress(addressId, updateAddress);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion

        }
        [Fact]
        public async Task DeleteAddress_ShouldReturnDeletedAddress_WhenAddressIsDeleted()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int addressId = 1;
            Address address = new()
            {
                Id = addressId,
                StreetName = "TestStreet",
                Number = 12,
                Floor = "2th",
                ZipCity = new ZipCity { Id = 2400, City = "TestBronx" },
                ZipCityId = 2400,
                Country = "Danmark",
                Customer = new Customer { Id = 2 },
                CustomerId = 2
            };
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            #endregion
            #region Act
            var result = await _sut.DeleteAddress(addressId);
            var deletedAddress = await _sut.GetAddressById(addressId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<Address>(result);
            Assert.Equal(addressId, result.Id);

            Assert.Null(deletedAddress);
            #endregion

            
        }
        [Fact]
        public async Task DeleteAddress_ShouldReturnNull_WhenAddressDoesNotExist()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int addressId = 1;
            #endregion
            #region Act
            var result = await _sut.DeleteAddress(addressId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }



        
    }
}
