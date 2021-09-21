using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database;
using WebShop.API.Database.Entities;

namespace WebShop.API.Repository
{
        public interface ICategoryRepository
        {
            Task<List<Category>> GetAllCategories();
            Task<Category> GetCategoryById(int categoryId);
            Task<Category> CreateCategory(Category category);
            Task<Category> UpdateCategory(int categoryId, Category category);
            Task<Category> DeleteCategory(int categoryId);
        }

        public class CategoryRepository : ICategoryRepository
        {
            private readonly WebShopContext _context;

            public CategoryRepository(WebShopContext context)
            {
                _context = context;

            }
            
            #region Get All Categories
            public async Task<List<Category>> GetAllCategories()
            {
                return await _context.Category
                    .ToListAsync();
            }
            #endregion
            #region Get Category by Id
            public async Task<Category> GetCategoryById(int categoryId)
            {
                return await _context.Category
                    .Include(a => a.products)
                    .FirstOrDefaultAsync(a => a.Id == categoryId);
            }
            #endregion
            #region Create Category
            public async Task<Category> CreateCategory(Category category)
            {
                _context.Category.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            #endregion
            #region Delete Category
            public async Task<Category> DeleteCategory(int categoryId)
            {
                Category category = await _context.Category.FirstOrDefaultAsync(a => a.Id == categoryId);
                if (category != null)
                {
                    _context.Category.Remove(category);
                    await _context.SaveChangesAsync();
                }
                return category;
            }
            #endregion
            #region Update Category

            public async Task<Category> UpdateCategory(int categoryId, Category category)
            {
                Category updateCategory = await _context.Category.FirstOrDefaultAsync(a => a.Id == categoryId);
                if (updateCategory != null)
                {
                    updateCategory.Name = category.Name;
                    updateCategory.Picture = category.Picture;
                    await _context.SaveChangesAsync();

                }
                return updateCategory;
            }
            #endregion
        }
    
}
