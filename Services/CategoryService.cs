using BlazorApp.Components.Pages.Categories.Forms;
using BlazorApp.Database.Interfaces;
using BlazorApp.Database.Models;
using BlazorApp.Services.Interfaces;

namespace BlazorApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(Category category)
        {
            await _categoryRepository.CreateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);

            if (category != null)
            {
                await _categoryRepository.DeleteAsync(category);
            }
        }

        public async Task<(IEnumerable<Category> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? search = null)
        {
            return await _categoryRepository.GetAllAsync(page, pageSize, search);
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<bool> GetByNameAsync(string name)
        {
            Category? category = await _categoryRepository.GetByNameAsync(name);
            return category != null ? true : false; 
        }

        public async Task UpdateAsync(int id, UpdateCategoryForm updateCategoryForm)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);

            if ( category != null)
            {
                category.Name = updateCategoryForm.Name;
                await _categoryRepository.UpdateAsync(category);
            }
        }
    }
}
