using CarDealershipApi.Dtos;
using CarDealershipApi.Models;

public class CarAdvertDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int Year { get; set; }
    public string Location { get; set; }
    public int MileAge { get; set; }
    public int Odometr { get; set; }
    public string GasType { get; set; }
    List<string> PhotoUrls { get; set; }
    List<CarDamageDto> Damages { get; set; }
}

public class CarAdvertCreateDto
{
    public string Name { get; set; }
    public int Year { get; set; }
    public string Location { get; set; }
    public int MileAge { get; set; }
    public int Odometr { get; set; }
    public string GasType { get; set; }
    public List<string> PhotoUrls { get; set; }
    public List<DamageCreateDto> Damages { get; set; }
}

public class DamageCreateDto
{
    public DamageType Type { get; set; }
    public string Description { get; set; }
    public SeverityLevel Severity { get; set; }
}