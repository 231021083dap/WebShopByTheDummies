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
    public class UserRepositoryTests
    {
        private DbContextOptions<WebShopContext> _options;
        private readonly WebShopContext _context;
        private readonly UserRepository _sut;


        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase(databaseName: "WebShopTestUser")
                .Options;

            _context = new WebShopContext(_options);

            _sut = new UserRepository(_context);
        }
        [Fact]
        public async Task GetAllUsers_ShouldReturnListOfUsers_WhenUsersExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.User.Add(new User
            {
                Id = 1,
                Email = "Test@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            });
            _context.User.Add(new User
            {
                Id = 2,
                Email = "Test2@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetAllUsers();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
            #endregion
        }
        [Fact]
        public async Task GetAllUsers_ShouldReturnEmptyListOfUsers_WhenNoUsersExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            #endregion
            #region Act
            var result = await _sut.GetAllUsers();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Empty(result);
            #endregion
        }

        [Fact]
        public async Task GetUserById_ShouldReturnTheUser_IfUserExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            _context.User.Add(new User
            {
                Id = userId,
                Email = "Test@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            });
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            var result = await _sut.GetByUserId(userId);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            #endregion
        }
        [Fact]
        public async Task GetUserById_ShouldReturnNull_IfUserDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            #endregion
            #region Act
            var result = await _sut.GetByUserId(userId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        [Fact]
        public async Task GetUserByEmail_ShouldReturnTheUser_IfUserExists()
        {
            
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            string userEmail = "Test@test.test";
            _context.User.Add(new User
            {
                Id = 1,
                Email = userEmail,
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            });
            await _context.SaveChangesAsync();

            #endregion
            #region Act
            var result = await _sut.GetByEmail(userEmail);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userEmail, result.Email);
            #endregion

        }
        [Fact]
        public async Task GetByEmail_ShouldReturnNull_IfEmailDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            string userEmail = "test@test.test";
            #endregion
            #region Act
            var result = await _sut.GetByEmail(userEmail);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
        [Fact]
        public async Task CreateUser_ShouldAddIdToUser_WhenSavingToDatebase()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            User user = new()
            {
                Email = "Test@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            };
            #endregion
            #region Act
            var result = await _sut.CreateUser(user);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedId, result.Id);
            #endregion
        }

        [Fact]
        public async Task CreateUser_ShouldFailToAddNewUser_WhenAddingUserWithExistingId()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();

            User user = new()
            {
                Id = 1,
                Email = "Test@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            #endregion
            #region Act
            Func<Task> action = async () => await _sut.CreateUser(user);
            #endregion
            #region Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("already been added", ex.Message);
            #endregion
        }

        [Fact]
        public async Task UpdateUser_ShouldChangeValuesOnUser_WhenUserExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User user = new()
            {
                Id = userId,
                Email = "Test@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            User updateUser = new()
            {
                Id = userId,
                Email = "Tes2t2@test.test",
                Password = "Passw0rd1337",
                Role = API.Helpers.Role.User
            };
            #endregion
            #region Act
            var result = await _sut.UpdateUser(userId, updateUser);
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(updateUser.Email, result.Email);
            Assert.Equal(updateUser.Password, result.Password);
            Assert.Equal(updateUser.Role, result.Role);
            #endregion
        }
        [Fact]
        public async Task UpdateUser_ShouldReturnNull_WhenUserDoesNotExists()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User updateUser = new()
            {
                Id = userId,
                Email = "Test@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            };
            #endregion
            #region Act
            var result = await _sut.UpdateUser(userId, updateUser);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnDeletedUser_WhenUserIsDeleted()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            User user = new()
            {
                Id = userId,
                Email = "Test@test.test",
                Password = "Passw0rd",
                Role = API.Helpers.Role.User
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            #endregion
            #region Act
            var result = await _sut.DeleteUser(userId);
            var deletedUser = await _sut.GetAllUsers();
            #endregion
            #region Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result.Id);

            Assert.Empty(deletedUser);
            #endregion
        }
        [Fact]
        public async Task DeleteUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            #region Arrange
            await _context.Database.EnsureDeletedAsync();
            int userId = 1;
            #endregion
            #region Act
            var result = await _sut.DeleteUser(userId);
            #endregion
            #region Assert
            Assert.Null(result);
            #endregion
        }
    }
}
