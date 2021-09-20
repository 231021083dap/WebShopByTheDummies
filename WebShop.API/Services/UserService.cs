using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Authorization;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;

namespace WebShop.API.Services
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse> GetByUserId(int userId);
        Task<LoginResponse> Authenticate(LoginRequest login);
        Task<UserResponse> Register(RegisterUser newUser);
        Task<UserResponse> Update(int userId, UpdateUser updateUser);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            List<User> users = await _userRepository.GetAllUsers();

            return users == null ? null : users.Select(u => new UserResponse
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public async Task<UserResponse> GetByUserId(int userId)
        {
            User user = await _userRepository.GetByUserId(userId);
            return userResponse(user);
        }

        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {

            User user = await _userRepository.GetByEmail(login.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password == login.Password)
            {
                LoginResponse response = new LoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }

            return null;
        }

        public async Task<UserResponse> Register(RegisterUser newUser)
        {
            User user = new User
            {
                Email = newUser.Email,
                Password = newUser.Password,
                Role = Helpers.Role.User // force all users created through Register, to Role.User
            };

            user = await _userRepository.CreateUser(user);

            return userResponse(user);
        }

        public async Task<UserResponse> Update(int userId, UpdateUser updateUser)
        {
            User user = new User
            {
                Email = updateUser.Email,
                Password = updateUser.Password,
                Role = updateUser.Role
            };

            user = await _userRepository.UpdateUser(userId, user);

            return userResponse(user);
        }

        private UserResponse userResponse(User user)
        {
            return user == null ? null : new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
