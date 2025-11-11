using BlazorApp.Database.Models;

namespace BlazorApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(Category category);
        Task<(IEnumerable<Category> Items, int TotalCount)> GetAllAsync(int page, int pageSize);
        Task<bool> GetByNameAsync(string name);
    }
}