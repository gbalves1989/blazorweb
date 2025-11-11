using BlazorApp.Database.Interfaces;
using BlazorApp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Category> Items, int TotalCount)> GetAllAsync(int page, int pageSize)
        {
            int total = await _context.Categories.CountAsync();

            var items = await _context.Categories
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, total);
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
