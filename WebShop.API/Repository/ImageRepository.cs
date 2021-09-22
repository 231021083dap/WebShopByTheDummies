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
    public class ImageRepository : IImageRepository
    {
        private readonly WebShopContext _context;

        public ImageRepository(WebShopContext context)
        {
            _context = context;

        }

        #region Get All Images
        public async Task<List<Image>> GetAllImages()
        {
            return await _context.Image
                .ToListAsync();
        }
        #endregion
        #region Get Image By Id
        public async Task<Image> GetImageById(int imageId)
        {
            return await _context.Image
                
                .FirstOrDefaultAsync(a => a.Id == imageId);
        }
        #endregion
        #region Create Image
        public async Task<Image> CreateImage(Image image)
        {
            _context.Image.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }
        #endregion
        #region Delete Image
        public async Task<Image> DeleteImage(int imageId)
        {
            Image image = await _context.Image.FirstOrDefaultAsync(i => i.Id == imageId);
            if (image != null)
            {
                _context.Image.Remove(image);
                await _context.SaveChangesAsync();
            }
            return image;
        }
        #endregion
    }

}
