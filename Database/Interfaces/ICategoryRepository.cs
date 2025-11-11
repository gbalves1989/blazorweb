using BlazorApp.Database.Models;

namespace BlazorApp.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);
        Task<(IEnumerable<Category> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? search = null);
        Task<Category?> GetByNameAsync(string name);
        Task<Category?> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
    }
}
