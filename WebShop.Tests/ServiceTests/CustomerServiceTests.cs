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
            #region Arrange
            int customerId = 1;

            User user = new User
            {
                Id = 1,
                Email = "test"
            };

            //List<Address> addresses = new List<Address>();
            //addresses.Add(new Address
            //{
            //    Id = 1,
            //    //CustomerId = 1,
            //    StreetName = "test",
            //    Number = 123,
            //    Floor = "1. TV",
            //    //ZipCity
            //    ZipCityId = 2100,
            //    Country = "Denmark"
            //});

            Customer customer = new Customer
            {
                Id = customerId,
                UserId = 1,
                FirstName = "test",
                MiddleName = "test",
                LastName = "test",
                User = user,
                Addresses = new()
                //Addresses = addresses
            };

            _customerRepository
                .Setup(a => a.GetCustomerById(It.IsAny<int>()))
                .ReturnsAsync(customer);

            //_userRepository
            //    .Setup(a => a.GetByUserId(It.IsAny<int>()))
            //    .ReturnsAsync(user);

            //_addressRepository
            //    .Setup(a => a.GetAddressById(It.IsAny<int>()))
            //    .ReturnsAsync(address);
            #endregion

            #region Act
            var result = await _sut.GetCustomerById(customerId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<CustomerResponse>(result);
            Assert.Equal(customerId, result.Id);
            Assert.Equal(customer.FirstName, result.FirstName);
            Assert.Equal(customer.MiddleName, result.MiddleName);
            Assert.Equal(customer.LastName, result.LastName);

            Assert.Equal(customer.User.Id, result.User.Id);
            Assert.Equal(customer.User.Email, result.User.Email);

            //coll(customer.Addresses, result.Addresses);
            #endregion
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
