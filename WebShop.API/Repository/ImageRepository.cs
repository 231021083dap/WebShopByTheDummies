using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{

        public interface IImageRepository
        {
            Task<List<Image>> GetAllImages();
            Task<Image> GetImageById(int imageId);
            Task<Image> CreateImage(Image image);
            Task<Image> DeleteImage(int imageId);
        }
    public class ImageRepository
    {
        public class AuthorRepository : IImageRepository
        {
            private readonly WebShopContext _context;

            public AuthorRepository(WebShopContext context)
            {
                _context = context;

            }


           public async Task<List<Image>> GetAllImages()
            {
                return await _context.Image
                    //.Include(a => a.Product)
                    .ToListAsync();
            }

            public async Task<Image> GetImageById(int imageId)
            {
                return await _context.Image
                    //.Include(a => a.Product)
                    .FirstOrDefaultAsync(a => a.Id == imageId);
            }

            public async Task<Image> CreateImage(Image image)
            {
                _context.Image.Add(image);
                await _context.SaveChangesAsync();
                return image;
            }

            public async Task<Image> DeleteImage(int imageId)
            {
                Image image = await _context.Image.FirstOrDefaultAsync(a => a.Id == imageId);
                if (image != null)
                {
                    _context.Image.Remove(image);
                    await _context.SaveChangesAsync();
                }
                return image;
            }
        }
    }
}
