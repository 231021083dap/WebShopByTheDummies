using Moq;
using WebShop.API.Authorization;
using WebShop.API.Database.Entities;
using WebShop.API.Repository;
using WebShop.API.Services;
using Xunit;

namespace WebShop.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IJwtUtils> _jwtUtils = new Mock<IJwtUtils>();
        private readonly Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();
        private readonly Mock<IAddressRepository> _addressRepository = new Mock<IAddressRepository>();

        public UserServiceTests()
        {
            _sut = new UserService(_userRepository.Object, _jwtUtils.Object, _customerRepository.Object, _addressRepository.Object);
        }

        #region GetAll
        [Fact]
        public async void GetAll_ShouldReturnListOfUserResponses_WhenUsersExist()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfUserResponses_WhenNoUsersExists()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnUserResponse_WhenUserExists()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region Authenticate (MISSING (TODO))

        #endregion

        #region Create/Register (MISSING (TODO))

        #endregion

        #region Update
        [Fact]
        public async void Update_ShouldReturnUpdateUserResponse_WhenUpdateIsSuccess()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenUserDoesNotExist()
        {
            #region Arrange
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion

        #region Delete
        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            #region Arrange
            int userId = 1;

            Address address = new Address
            {
                CustomerId = 1,
                Id = 1,
                StreetName = "test",
                Number = 123,
                Floor = "1. TV",
                ZipCity = new ZipCity { Id = 2100, City = "Østerbro"},
                ZipCityId = 2100,
                Country = "Denmark"
            };

            User user = new User
            {
                Id = userId,
                Email = "test",
                Password = "test",
                Customer = new Customer 
                { 
                    UserId = userId, 
                    Id = 1, 
                    FirstName = "test",
                    MiddleName = "test", 
                    LastName = "test"
                }
            };

            _userRepository
                .Setup(a => a.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(user);
            #endregion

            #region Act
            var result = await _sut.Delete(userId);
            #endregion

            #region Assert
            Assert.True(result);
            #endregion
        }
        #endregion
    }
}
