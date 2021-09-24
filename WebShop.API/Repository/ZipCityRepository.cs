using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IZipCityRepository {
        Task<List<ZipCity>> GetAllZipCodes();
        Task<ZipCity> GetZipCityById(int Id);
    }
    public class ZipCityRepository : IZipCityRepository
    {
        private readonly WebShopContext _context;
        public ZipCityRepository(WebShopContext context)
        {
            _context = context;
        }
        public async Task<List<ZipCity>> GetAllZipCodes()
        {
            return await _context.ZipCity
                .ToListAsync();
        }
        public async Task<ZipCity> GetZipCityById(int Id)
        {
            return await _context.ZipCity
                .FirstOrDefaultAsync(a => a.Id == Id);
        }
    }
}
