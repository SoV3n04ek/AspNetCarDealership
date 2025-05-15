using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // Enable API controllers

// Add PostgreSQL (corrected from UseNpsql to UseNpgsql)
builder.Services.AddDbContext<CarDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register your service (replace CarAdvertService with your actual service)
builder.Services.AddScoped<ICarService, CarService>();

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Maps endpoints from controllers

// Seed initial data (optional - remove in production)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CarDbContext>();
    db.Database.EnsureCreated();
    
    if (!db.Cars.Any())
    {
        db.Cars.AddRange(
            new Car { Name = "Toyota Camry", Year = 2022, Status = "New" },
            new Car { Name = "Honda Civic", Year = 2020, Status = "Used" }
        );
        await db.SaveChangesAsync();
    }
}

app.Run();

// Move these to separate files in a real project
public record Car(int Id, string Name, int Year, string Status);

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) {}
    public DbSet<Car> Cars => Set<Car>();
}

// Service interface/implementation (add to Services/CarService.cs later)
public interface ICarService
{
    Task<List<Car>> GetAllCarsAsync();
}

public class CarService : ICarService
{
    private readonly CarDbContext _db;
    
    public CarService(CarDbContext db) => _db = db;
    
    public async Task<List<Car>> GetAllCarsAsync() => 
        await _db.Cars.ToListAsync();
}