using CarDealershipApi.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<CarAdvert> CarAdverts { get; set; }
    public DbSet<CarDamage> Damages { get; set; } 

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity <CarDamage>()
            .HasOne(cd => cd.CarAdvert)
            .WithMany(car => car.Damages)
            .HasForeignKey(cd => cd.CarAdvertId);
        
        modelBuilder.Entity<CarDamage>()
            .HasIndex(cd => cd.CarAdvertId); 

        modelBuilder.Entity<CarDamage>()
            .HasIndex(cd => cd.Type); 

        base.OnModelCreating(modelBuilder);

    }
}