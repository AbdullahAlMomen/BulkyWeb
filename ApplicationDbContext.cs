using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { 
        }
        public DbSet<Category>Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category {Id=1,Name="Action1",DispleyOrder=1 },
                new Category {Id=2,Name="Scifi",DispleyOrder=2 },
                new Category {Id=3,Name="History",DispleyOrder=3 }
                );
        }
    }
}
