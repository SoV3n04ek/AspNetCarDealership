var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cars", () =>
{
    var cars = new List<Car>
    {
        new Car("Tesla Model S", "https://example.com/tesla.jpg", 79990),
        new Car("Ford Mustang", "https://example.com/ford.jpg", 42995),
        new Car("Toyota Camry", "https://example.com/toyota.jpg", 25945)
    };

    return Results.Ok(cars);
});

app.Run();

public record Car(string Name, string PhotoUrl, decimal Price);