using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CarAdvertsController : ControllerBase
{
    private readonly ICarService _service;

    public CarAdvertsController(ICarService service) => _service = service;


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await _service.GetByIdAsync(id);
        return car == null ? NotFound() : Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CarAdvert car)
    {
        var created = await _service.CreateAsync(car);
        return CreatedAtAction(nameof(GetById),
                               new { id = created.Id }, 
                               created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CarAdvert car)
    {
        return await _service.UpdateAsync(id, car)
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _service.DeleteAsync(id)
            ? NoContent()
            : NotFound();
    }

    /*[HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
    {
        var cars = await _service.GetAllCarsAsync(page, pageSize);
        return Ok(cars);
    }*/
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllCarsAsync());

    [HttpGet("paginated")] // Теперь будет GET /api/CarAdverts/paginated?page=1&pageSize=10
    public async Task<IActionResult> GetAllPaginated(int page = 1, int pageSize = 10)
    {
        var result = await _service.GetAllCarsAsync(page, pageSize);
        return Ok(result);
    }
}