using BlazorApp.Database.Models;

namespace BlazorApp.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);
        Task<(IEnumerable<Category> Items, int TotalCount)> GetAllAsync(int page, int pageSize);
        Task<Category?> GetByNameAsync(string name);
    }
}
