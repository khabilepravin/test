using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace DataAccess;

 public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Movies");
    }

    public DbSet<Movie> Movies { get; set; }
}


