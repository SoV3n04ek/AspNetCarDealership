public interface ICarService
{
    Task<List<Car>> GetAllCarsAsync();
}

public class CarService : ICarService
{
    private readonly CarDbContext _db;
    public CarService(CarDbContext db) => _db = db;

    public async Task<List<Car>> GetAllCarsAsync() 
        => await _db.Cars.ToListAsync();
}
