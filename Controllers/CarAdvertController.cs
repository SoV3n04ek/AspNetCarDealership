[ApiController]
[Route("api/[controller]")]
public class CarAdvertController : ControllerBase
{
    private readonly CarAdvertService _service;

    public CarAdvertController(CarAdvertService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var cars = await _service.GetAllAsync(status, page, pageSize);
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var car = await _service.GetByIdAsync(id);
        return car == null ? NotFound() : Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CarAdvert car)
    {
        await _service.AddAsync(car);
        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }
}