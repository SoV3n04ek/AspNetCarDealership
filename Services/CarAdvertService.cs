public class CarAdvertService
{
    private readonly AppDbContext _db;

    public CarAdvertService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CarAdvert>> GetAllAsync(string? statusFilter, int page = 1, int pageSize = 10)
    {
        var query = _db.CarAdverts.AsQueryable();

        if (!string.IsNullOrEmpty(statusFilter))
            query = query.Where(c => c.Status == statusFilter);

        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<CarAdvert?> GetByIdAsync(int id) => 
        await _db.CarAdverts.FindAsync(id);

    public async Task AddAsync(CarAdvert car) 
    {
        _db.CarAdverts.Add(car);
        await _db.SaveChangesAsync();
    }
}