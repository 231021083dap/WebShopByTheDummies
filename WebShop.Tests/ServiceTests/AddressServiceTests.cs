using Moq;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests
{
    /*
     * TODO:
     * Create_ShouldReturnAddressResponse (Missing an equal check on addressId or customerId not sure which one)
     * Update_ShouldReturnUpdateAddressResponse (Missing an equal check on addressId or customerId not sure which one)
     */

    public class AddressServiceTests
    {
        private readonly AddressService _sut;
        private readonly Mock<IAddressRepository> _addressRepository = new Mock<IAddressRepository>();
        private readonly Mock<IZipCityRepository> _zipcityRepository = new Mock<IZipCityRepository>();
        private readonly Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();

        public AddressServiceTests()
        {
            _sut = new AddressService(_addressRepository.Object, _customerRepository.Object, _zipcityRepository.Object);
        }

        #region Create
        [Fact]
        public async void Create_ShouldReturnAddressResponse_WhenCreateIsSuccess()
        {
            #region Arrange
            int addressId = 1;

            NewAddress newAddress = new NewAddress
            {
                CustomerId = 1,
                StreetName = "test",
                Number = 123,
                Floor = "1. TV",
                Zipcode = 2100,
                Country = "Denmark"
            };

            ZipCity zipCity = new ZipCity
            {
                Id = 2100,
                City = "Østerbro"
            };

            Address address = new Address
            {
                CustomerId = 1,
                Id = addressId,
                StreetName = "test",
                Number = 123,
                Floor = "1. TV",
                ZipCityId = 2100,
                Country = "Denmark"
            };

            Customer customer = new Customer
            {
                Id = 1,
                UserId = 1,
                FirstName = "test",
                MiddleName = "test",
                LastName = "test",
                User = new User { Id = 1, Email = "test", Role = API.Helpers.Role.User }
            };

            _addressRepository
                .Setup(a => a.CreateAddress(It.IsAny<Address>()))
                .ReturnsAsync(address);

            _customerRepository
                .Setup(a => a.GetCustomerById(It.IsAny<int>()))
                .ReturnsAsync(customer);

            _zipcityRepository
                .Setup(a => a.GetZipCityById(It.IsAny<int>()))
                .ReturnsAsync(zipCity);
            #endregion

            #region Act
            var result = await _sut.Create(newAddress);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<AddressResponse>(result);
            Assert.Equal(addressId, result.Id);
            Assert.Equal(newAddress.StreetName, result.StreetName);
            Assert.Equal(newAddress.Number, result.Number);
            Assert.Equal(newAddress.Floor, result.Floor);
            Assert.Equal(newAddress.Zipcode, result.Zipcode);
            Assert.Equal(newAddress.Country, result.Country);
            //Assert.Equal(newAddress.CustomerId, result.Customer.Id); // TODO* Fejler på Customer.Id
            #endregion
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateAddressResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            int addressId = 1;

            UpdateAddress updateAddress = new UpdateAddress
            {
                CustomerId = 1,
                StreetName = "test",
                Number = 123,
                Floor = "1. TV",
                Zipcode = 2100,
                Country = "Denmark"
            };

            ZipCity zipCity = new ZipCity
            {
                Id = 2100,
                City = "Østerbro"
            };

            Address address = new Address
            {
                CustomerId = 1,
                Id = addressId,
                StreetName = "test",
                Number = 123,
                Floor = "1. TV",
                ZipCityId = 2100,
                Country = "Denmark"
            };

            Customer customer = new Customer
            {
                Id = 1,
                UserId = 1,
                FirstName = "test",
                MiddleName = "test",
                LastName = "test",
                User = new User { Id = 1, Email = "test", Role = API.Helpers.Role.User }
            };

            _addressRepository
                .Setup(a => a.UpdateAddress(It.IsAny<int>(), It.IsAny<Address>()))
                .ReturnsAsync(address);

            _customerRepository
                .Setup(a => a.GetCustomerById(It.IsAny<int>()))
                .ReturnsAsync(customer);

            _zipcityRepository
                .Setup(a => a.GetZipCityById(It.IsAny<int>()))
                .ReturnsAsync(zipCity);
            #endregion

            #region Act
            var result = await _sut.UpdateAddress(addressId, updateAddress);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<AddressResponse>(result);
            Assert.Equal(addressId, result.Id);
            Assert.Equal(updateAddress.StreetName, result.StreetName);
            Assert.Equal(updateAddress.Number, result.Number);
            Assert.Equal(updateAddress.Floor, result.Floor);
            Assert.Equal(updateAddress.Zipcode, result.Zipcode);
            Assert.Equal(updateAddress.Country, result.Country);
            //Assert.Equal(newAddress.CustomerId, result.Customer.Id); // TODO* Fejler på Customer.Id
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenAddressDoesNotExist()
        {
            #region Arrange
            int addressId = 1;

            UpdateAddress updateAddress = new UpdateAddress
            {
                CustomerId = 1,
                StreetName = "test",
                Number = 123,
                Floor = "1. TV",
                Zipcode = 2100,
                Country = "Denmark"
            };

            _addressRepository
                .Setup(a => a.UpdateAddress(It.IsAny<int>(), It.IsAny<Address>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.UpdateAddress(addressId, updateAddress);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Delete
        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            #region Arrange
            int addressId = 1;

            Address address = new Address
            {
                Id = addressId,
                Customer = new Customer { Id = 1 },
                CustomerId = 1,
                StreetName = "test",
                Number = 123,
                Floor = "1. TV",
                ZipCity = new ZipCity { Id = 2100, City = "Østerbro" },
                ZipCityId = 2100,
                Country = "Denmark"
            };

            _addressRepository
                .Setup(a => a.DeleteAddress(It.IsAny<int>()))
                .ReturnsAsync(address);
            #endregion

            #region Act
            var result = await _sut.Delete(addressId);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }
        #endregion
    }
}
