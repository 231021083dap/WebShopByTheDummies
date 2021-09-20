using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using static WebShop.API.Repository.CategoryRepository;

namespace WebShop.API.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> GetCategoryById(int categoryId);
        Task<CategoryResponse> CreateCategory(NewCategory newCategory);
        Task<CategoryResponse> UpdateCategory(int categoryId, UpdateCategory updateCategory);
        Task<bool> DeleteCategory(int categoryId);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        #region Get All Categories
        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<Category> categories = await _categoryRepository.GetAllCategories();
            return categories == null ? null : categories.Select(a => new CategoryResponse
            {
                Id = a.Id,
                Name = a.Name,
                Picture = a.Picture
            }).ToList();
        }
        #endregion

        public async Task<CategoryResponse> GetCategoryById(int categoryId)
        {
            Category category = await _categoryRepository.GetCategoryById(categoryId);
            return category == null ? null : new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Picture = category.Picture
            };
        }

        public Task<CategoryResponse> CreateCategory(NewCategory newCategory)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse> UpdateCategory(int categoryId, UpdateCategory updateCategory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        
    }
}
