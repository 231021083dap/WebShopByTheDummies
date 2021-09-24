using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.API.Database.Entities;
using WebShop.API.DTO.Requests;
using WebShop.API.DTO.Responses;
using WebShop.API.Repository;

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
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
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

        #region Get Category By Id
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
        #endregion

        #region Create Category
        public async Task<CategoryResponse> CreateCategory(NewCategory newCategory)
        {
            Category category = new Category
            {
                Name = newCategory.Name,
                Picture = newCategory.Picture
            };
            category = await _categoryRepository.CreateCategory(category);
            return category == null ? null : new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Picture = category.Picture
            };
        }
        #endregion

        #region Update Category
        public async Task<CategoryResponse> UpdateCategory(int categoryId, UpdateCategory updateCategory)
        {
            Category category = new Category
            {
                Name = updateCategory.Name,
                Picture = updateCategory.Picture
            };
            category = await _categoryRepository.UpdateCategory(categoryId, category);

            return category == null ? null : new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Picture = category.Picture
            };
        }
        #endregion

        #region Delete Category
        public async Task<bool> DeleteCategory(int categoryId)
        {
            var result = await _categoryRepository.DeleteCategory(categoryId);
            return true;
        }
        #endregion
    }
}