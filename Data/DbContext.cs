using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CarAdvert> CarAdverts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}