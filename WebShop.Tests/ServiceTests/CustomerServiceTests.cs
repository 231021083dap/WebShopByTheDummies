using Moq;
using System.Collections.Generic;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests
{
    /*TODO:
     * GetById_ShouldReturnAnCustomerResponse (Issue with id)
     * Update_ShouldReturnUpdateCustomerResponse (Issue with id)
     */
    public class CustomerServiceTests
    {
        private readonly CustomerService _sut;
        private readonly Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IAddressRepository> _addressRepository = new Mock<IAddressRepository>();

        public CustomerServiceTests()
        {
            _sut = new CustomerService(_customerRepository.Object, _userRepository.Object, _addressRepository.Object);
        }

        #region GetAll
        [Fact]
        public async void GetAll_ShouldReturnListOfCustomerResponses_WhenCustomersExist()
        {
            #region Arrange
            List<Customer> Customers = new List<Customer>();
            Customers.Add(new Customer
            {
                Id = 1,
                FirstName = "test",
                MiddleName = "test",
                LastName = "test",
                User = new User { Id = 1, Email = "test" }
            });

            Customers.Add(new Customer
            {
                Id = 2,
                FirstName = "test",
                MiddleName = "test",
                LastName = "test",
                User = new User { Id = 2, Email = "test" }
            });

            _customerRepository
                .Setup(a => a.GetAllCustomers())
                .ReturnsAsync(Customers);
            #endregion

            #region Act
            var result = await _sut.GetAllCustomers();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<CustomerResponse>>(result);
            #endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfCustomerResponses_WhenNoCustomersExists()
        {
            #region Arrange
            List<Customer> Customers = new List<Customer>();

            _customerRepository
                .Setup(a => a.GetAllCustomers())
                .ReturnsAsync(Customers);
            #endregion

            #region Act
            var result = await _sut.GetAllCustomers();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<CustomerResponse>>(result);
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnCustomerResponse_WhenCustomerExists()
        {

            //#region Arrange
            //int userId = 1;

            //User user = new User
            //{
            //    Id = userId,
            //    Email = "test"
            //};

            //Customer customer = new Customer
            //{
            //    Id = 1,
            //    UserId = userId,
            //    FirstName = "test",
            //    MiddleName = "test",
            //    LastName = "test",
            //    Addresses = new()
            //};


            ////List<Address> addresses = new List<Address>();
            ////addresses.Add(new Address
            ////{
            ////    Id = 1,
            ////    CustomerId = customerId,
            ////    StreetName = "test",
            ////    Number = 123,
            ////    Floor = "1. TV",
            ////    ZipCity = { Id = 2100, City = "Østerbro" },
            ////    ZipCityId = 2100,
            ////    Country = "Denmark"
            ////});

            ////Address address = new Address
            ////{
            ////    Id = 1,
            ////    StreetName = "test",
            ////    Number = 123,
            ////    Floor = "1. TV",
            ////    ZipCity = { Id = 2100, City = "Østerbro" },
            ////    ZipCityId = 2100,
            ////    Country = "Denmark"
            ////};

            //_userRepository
            //    .Setup(a => a.GetByUserId(It.IsAny<int>()))
            //    .ReturnsAsync(user);

            //_customerRepository
            //    .Setup(a => a.GetCustomerById(It.IsAny<int>()))
            //    .ReturnsAsync(customer);

            ////_addressRepository
            ////    .Setup(a => a.GetAddressById(It.IsAny<int>()))
            ////    .ReturnsAsync(address);

            //#endregion

            //#region Act
            //var result = await _sut.GetCustomerById(userId);
            //#endregion

            //#region Assert
            //Assert.NotNull(result);
            //Assert.IsType<CustomerResponse>(result);
            //Assert.Equal(userId, result.Id);

            //#endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            #region Arrange
            int customerId = 1;

            _customerRepository
                .Setup(a => a.GetCustomerById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.GetCustomerById(customerId);
            #endregion

            #region Assert
            Assert.Null(result);
            #endregion
        }
        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateCustomerResponse_WhenUpdateIsSuccess()
        {
            //MISSING ID!

            //#region Arrange
            
            //int customerId = 1;

            //User user = new User
            //{
            //    Id = 1,
            //    Customer = new Customer { Id = 1, FirstName = "test", MiddleName = "test", LastName = "test" },
            //    Email = "test",
            //    Password = "test",
            //    Role = API.Helpers.Role.User
            //};

            //Customer customer = new Customer
            //{
            //    UserId = 1,
            //    Id = customerId,
            //    FirstName = "test",
            //    MiddleName = "test",
            //    LastName = "test",
            //    User = new User { Id = 1, Email = "test" }
            //};

            //UpdateCustomer updateCustomer = new UpdateCustomer
            //{
            //    FirstName = "test",
            //    MiddleName = "test",
            //    LastName = "test",
            //    User =
            //    {
            //        Email = "test",
            //        Password = "test"
            //    }
            //};

            //_customerRepository
            //    .Setup(a => a.UpdateCustomer(It.IsAny<int>(), It.IsAny<Customer>()))
            //    .ReturnsAsync(customer);


            //_userRepository
            //    .Setup(a => a.GetByUserId(It.IsAny<int>()))
            //    .ReturnsAsync(user);
            //#endregion

            //#region Act
            //var result = await _sut.UpdateCustomer(customerId, updateCustomer);
            //#endregion

            //#region Assert
            //Assert.NotNull(result);
            //Assert.IsType<CustomerResponse>(result);
            //Assert.Equal(customerId, result.Id);
            //Assert.Equal(updateCustomer.FirstName, result.FirstName);
            //Assert.Equal(updateCustomer.LastName, result.LastName);
            //Assert.Equal(updateCustomer.MiddleName, result.MiddleName);
            //#endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            #region Arrange
            int customerId = 1;

            UpdateCustomer updateCustomer = new UpdateCustomer
            {
                FirstName = "test",
                MiddleName = "test",
                LastName = "test"
            };

            _customerRepository
                .Setup(a => a.UpdateCustomer(It.IsAny<int>(), It.IsAny<Customer>()))
                .ReturnsAsync(() => null);
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
