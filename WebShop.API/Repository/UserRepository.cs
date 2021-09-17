using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetByEmail(string email);
        Task<User> GetByUserId(int userId);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int userId, User user);
        Task<User> DeleteUser(int userId);
    }
    public class UserRepository : IUserRepository
    {
        private readonly WebShopContext _context;
        public UserRepository(WebShopContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User
                .ToListAsync();
        }
        public async Task<User> GetByEmail(string Email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == Email);
        }
        public async Task<User> GetByUserId(int userId)
        {
            return await _context.User
                .FirstOrDefaultAsync(a => a.Id == userId);
        }
        public async Task<User> CreateUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUser(int userId, User user)
        {
            User updateUser = await _context.User.FirstOrDefaultAsync(a => a.Id == userId);
            if (updateUser != null)
            {
                updateUser.Email = user.Email;
                updateUser.Password = user.Password;
                //updateUser.Role = user.Role;
                await _context.SaveChangesAsync();

            }
            return updateUser;
        }
        public async Task<User> DeleteUser(int userId)
        {
            User user = await _context.User.FirstOrDefaultAsync(a => a.Id == userId);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}
