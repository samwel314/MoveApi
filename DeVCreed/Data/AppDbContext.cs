using DeVCreed.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeVCreed.Data
{
    public class AppDbContext : DbContext
    {
           public AppDbContext(DbContextOptions options) 
            : base(options)
           {
           }

          public DbSet<Genre> Genres { get; set; }

          public DbSet<Movie>  Movies { get; set; }



          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
