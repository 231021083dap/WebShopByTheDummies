using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
    public interface IZipCityRepository {
        Task<List<ZipCity>> GetAllZipCodes(int zipcodeId);
    }
    public class ZipCityRepository : IZipCityRepository
    {
        private readonly WebShopContext _context;
        public ZipCityRepository(WebShopContext context)
        {
            _context = context;
        }

        public async Task<List<ZipCity>> GetAllZipCodes(int zipcodeId)
        {
            return await _context.ZipCity
                .ToListAsync();
        }
    }
}
