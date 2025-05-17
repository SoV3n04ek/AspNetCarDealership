using Microsoft.EntityFrameworkCore;

public interface ICarService
{
    Task<List<CarAdvert>> GetAllCarsAsync();
    Task<CarAdvert?> GetByIdAsync(int id);
    Task<CarAdvert> CreateAsync(CarAdvert car);
    Task<bool> UpdateAsync(int id, CarAdvert car);
    Task<bool> DeleteAsync(int id);
}

public class CarService : ICarService
{
    private readonly AppDbContext _db;

    public CarService(AppDbContext db) => _db = db;

    public async Task<List<CarAdvert>> GetAllCarsAsync() =>
        await _db.CarAdverts.ToListAsync();

    public async Task<CarAdvert?> GetByIdAsync(int id) =>
        await _db.CarAdverts.FindAsync(id);
            
    public async Task<CarAdvert> CreateAsync(CarAdvert car)
    {
        _db.CarAdverts.Add(car);
        await _db.SaveChangesAsync();
        return car;
    }

    public async Task<bool> UpdateAsync(int id, CarAdvert car)
    {
        var existing = await _db.CarAdverts.FindAsync(id);
        if (existing == null) return false;

        _db.Entry(existing).CurrentValues.SetValues(car);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var car = await _db.CarAdverts.FindAsync(id);
        if (car == null) return false;

        _db.CarAdverts.Remove(car);
        return await _db.SaveChangesAsync() > 0;
    }
}