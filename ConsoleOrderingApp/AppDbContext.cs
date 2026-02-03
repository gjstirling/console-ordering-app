using Microsoft.EntityFrameworkCore;
using ConsoleOrderingApp.Models;

namespace ConsoleOrderingApp;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}