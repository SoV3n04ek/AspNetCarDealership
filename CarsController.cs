[ApiController]
[Route("api/cars")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService) 
        => _carService = carService;

    [HttpGet]
    public async Task<IActionResult> GetAllCars() 
        => Ok(await _carService.GetAllCarsAsync());
}
