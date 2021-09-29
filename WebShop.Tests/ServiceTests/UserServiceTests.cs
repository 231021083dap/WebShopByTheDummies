using Moq;
using System.Collections.Generic;
using WebShop.API.Authorization;
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
     * GetById_ShouldReturnAnUserResponse (Can't test equal on password but is it needed?)
     * Update_ShouldReturnUpdateUserResponse (Can't test equal on password, this is needed. demands change to userResponse but may effect other things*)
     * Authenticate
     * Register (This should be a simpel create i will look into it)
     */

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
            List<User> users = new List<User>();
            users.Add(new User
            {
                Id = 1,
                Email = "test",
                Password = "test",
                Role = API.Helpers.Role.Admin
            });

            users.Add(new User
            {
                Id = 2,
                Email = "test",
                Password = "test",
                Role = API.Helpers.Role.User
            });

            _userRepository
                .Setup(a => a.GetAllUsers())
                .ReturnsAsync(users);
            #endregion

            #region Act
            var result = await _sut.GetAllUsers();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<UserResponse>>(result);
            #endregion
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfUserResponses_WhenNoUsersExists()
        {
            #region Arrange
            List<User> Users = new List<User>();

            _userRepository
                .Setup(a => a.GetAllUsers())
                .ReturnsAsync(Users);
            #endregion

            #region Act
            var result = await _sut.GetAllUsers();
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<UserResponse>>(result);
            #endregion
        }
        #endregion

        #region GetById
        [Fact]
        public async void GetById_ShouldReturnAnUserResponse_WhenUserExists()
        {
            #region Arrange
            int userId = 1;

            User user = new User
            {
                Id = userId,
                Email = "test",
                Password = "test",
                Role = API.Helpers.Role.User
            };

            _userRepository
                .Setup(a => a.GetByUserId(It.IsAny<int>()))
                .ReturnsAsync(user);
            #endregion

            #region Act
            var result = await _sut.GetByUserId(userId);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
            //Assert.Equal(user.Password, result.Password); //Not in userResponse
            Assert.Equal(user.Role, result.Role);
            #endregion
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenUserDoesNotExist()
        {
            #region Arrange
            int userId = 1;

            _userRepository
                .Setup(a => a.GetByUserId(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.GetByUserId(userId);
            #endregion

            #region Assert
            Assert.Null(result);
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
            UpdateUser updateUser = new UpdateUser
            {
                Email = "test",
                Password = "test",
            };

            int userId = 1;

            User user = new User
            {
                Id = userId,
                Email = "test",
                Password = "test",
                Role = API.Helpers.Role.User
            };

            _userRepository
                .Setup(a => a.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(user);
            #endregion

            #region Act
            var result = await _sut.Update(userId, updateUser);
            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(updateUser.Email, result.Email);
            //Assert.Equal(updateUser.Password, result.Password); //Not in userResponse
            #endregion
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenUserDoesNotExist()
        {
            #region Arrange
            int userId = 1;

            UpdateUser updateUser = new UpdateUser
            {
                Email = "test",
                Password = "test",
                Role = API.Helpers.Role.User
            };

            _userRepository
                .Setup(a => a.UpdateUser(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(() => null);
            #endregion

            #region Act
            var result = await _sut.Update(userId, updateUser);
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
