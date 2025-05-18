using CarDealershipApi.Data;
using CarDealershipApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
         builder.Services.AddControllers() // Enable API controllers
            .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
             });

        

        builder.Services.AddEndpointsApiExplorer();
        // Swagger
        builder.Services.AddSwaggerGen();

        // SQLITE connection
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information));

        // Register your service (replace CarAdvertService with your actual service)
        builder.Services.AddScoped<ICarService, CarService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Middleware pipeline
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers(); // Maps endpoints from controllers

        // Seed initial data (optional - remove in production)
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //db.Database.EnsureCreated();
            // EDIT
            db.Database.EnsureDeleted(); // Deleting already existing db
            db.Database.EnsureCreated();

            // Seeding DB on first startup
            //await DatabaseSeeder.Seed(db);

            // Добавьте тестовые данные
            if (!db.CarAdverts.Any())
            {
               for (int i =0;i<400;++i)
                {
                    var testCar = new CarAdvert
                    {
                        Name = "Toyota Camry 2022",
                        Year = 2022,
                        Location = "New York",
                        MileAge = 15000,
                        Odometr = 15000,
                        GasType = "Gasoline",
                        PhotoUrls = new List<string> { "https://example.com/photo1.jpg" },
                        Damages = new List<CarDamage>
                    {
                        new()
                        {
                            Type = DamageType.MINOR_DENT_SCRATCHES,
                            Severity = SeverityLevel.Low,
                            Description = "Небольшая царапина на двери"
                        },
                        new()
                        {
                            Type = DamageType.BURN_ENGINE,
                            Severity = SeverityLevel.High,
                            Description = "Требуется замена двигателя"
                        }
                    }
                    };

                    db.CarAdverts.Add(testCar);

                }
                await db.SaveChangesAsync();
            }
        }

        app.Run();
    }
}