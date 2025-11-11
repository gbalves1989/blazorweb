using BlazorApp.Components.Pages.Categories.Forms;
using BlazorApp.Database.Models;

namespace BlazorApp.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(Category category);
        Task<(IEnumerable<Category> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string? search = null);
        Task<bool> GetByNameAsync(string name);
        Task<Category?> GetByIdAsync(int id);
        Task UpdateAsync(int id, UpdateCategoryForm updateCategoryForm);
        Task DeleteAsync(int id);
    }
}