using Moq;
using WebShop.API.Authorization;
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

        #region Register (MISSING (TODO))

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
            #endregion

            #region Act
            #endregion

            #region Assert
            #endregion
        }
        #endregion
    }
}
