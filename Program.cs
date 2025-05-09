var builder = WebApplication.CreateBuilder(args);

// Add PostgreSQL configuration
builder.Services.AddDbContext<CarDbContext>(options => 
    options.UseNpsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.MapGet("/cars", async (CarDbContext dbContext) => 
{
    // Now using database instead of hardcoded list
    return Results.Ok(await dbContext.Cars.ToListAsync());
});

app.Run();

// Move these to separate files in a real project
public record Car(string Name, string PhotoUrl, decimal Price);

public class CarDbContext : DbContext
{
    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) {}
    public DbSet<Car> Cars => Set<Car>();
}
