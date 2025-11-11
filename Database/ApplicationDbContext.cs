using BlazorApp.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
    }
}
