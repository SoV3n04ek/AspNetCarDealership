using CarDealershipApi.Dtos;
using Microsoft.EntityFrameworkCore;

public interface ICarService
{
    //Task<List<CarAdvert>> GetAllCarsAsync();
    Task<CarAdvert?> GetByIdAsync(int id);
    Task<CarAdvert> CreateAsync(CarAdvert car);
    Task<bool> UpdateAsync(int id, CarAdvert car);
    Task<bool> DeleteAsync(int id);
    //Task<List<CarAdvert>> GetAllCarsAsync(int page = 1, int pageSize = 10);
    Task<PagedResponse<CarAdvert>> GetAllCarsAsync(int page = 1, int pageSize = 10);
}

public class CarService : ICarService
{
    private readonly AppDbContext _db;

    public CarService(AppDbContext db) => _db = db;

    public async Task<List<CarAdvert>> GetAllCarsAsync() =>
        await _db.CarAdverts
        .Include(c => c.Damages)
        .ToListAsync();

    public async Task<CarAdvert?> GetByIdAsync(int id) =>
        await _db.CarAdverts.Include(c => c.Damages)
            .FirstOrDefaultAsync(c => c.Id == id);
            
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

    //public async Task<List<CarAdvert>> GetAllCarsAsync(int page = 1, int pageSize = 10)
    //{
    //    return await _db.CarAdverts
    //    .Include(c => c.Damages)
    //    .OrderBy(c => c.Id) 
    //    .Skip((page - 1) * pageSize)
    //    .Take(pageSize)
    //    .ToListAsync();
    //}

    public async Task<PagedResponse<CarAdvert>> GetAllCarsAsync(int page = 1, int pageSize = 10)
    {
        // limit of max size of page
        pageSize = Math.Min(pageSize, 50);

        var query = _db.CarAdverts.Include(c => c.Damages);

        return new PagedResponse<CarAdvert>
        {
            Items = await query
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),
            TotalCount = await query.CountAsync(),
            Page = page,
            PageSize = pageSize
        };
    }

}