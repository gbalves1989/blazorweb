using BlazorApp.Database.Models;

namespace BlazorApp.Database.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetByNameAsync(string name);
        Task<(IEnumerable<Product> Items, int TotalCount)> GetAllByCategoryAsync(int idCategory, int page, int pageSize, string? search = null);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
