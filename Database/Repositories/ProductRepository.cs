using BlazorApp.Database.Interfaces;
using BlazorApp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetAllByCategoryAsync(int idCategory, int page, int pageSize, string? search = null)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(search.ToLower()) && c.CategoryId == idCategory);
            }

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _context.Products.Where(p => p.Name == name).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
