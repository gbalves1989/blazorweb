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

        public async Task<(IEnumerable<Category> Items, int TotalCount)> GetAllAsync(int page, int pageSize)
        {
            var (categories, total) = await _categoryRepository.GetAllAsync(page, pageSize);

            IEnumerable<Category> categoryList = categories.Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
            });

            return (categoryList, total);
        }

        public async Task<bool> GetByNameAsync(string name)
        {
            Category? category = await _categoryRepository.GetByNameAsync(name);
            return category != null ? true : false; 
        }
    }
}
