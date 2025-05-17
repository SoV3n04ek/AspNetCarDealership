using CarDealershipApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CarAdvert
{
    [ScaffoldColumn(false)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Car Name is required")]
    [StringLength(100, 
        MinimumLength = 11,
        ErrorMessage = "Name should be between 11 and 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Car year is required")]
    public int Year { get; set; }

    //[Required(ErrorMessage = "Car VehicleConditionType is required")]
    //public string VehicleConditionType { get; set; }

    public List<CarDamage> Damages { get; set; } = new();

    
    [Required(ErrorMessage = "Car Name is required")]
    public string Location { get; set; }
    
    [Required(ErrorMessage = "Car Mile age is required")]
    public int MileAge { get; set; }

    public int Odometr { get; set; }

    [Required(ErrorMessage = "Car GasType is required")]
    public string GasType { get; set; }
    public  List<string> PhotoUrls { get; set; }

    public CarAdvert() => PhotoUrls = [];

    public CarAdvert(string Name,
                     int Year,
                     string VehicleConditionType,
                     string Location,
                     int MileAge,
                     int Odometr,
                     string GasType)
    {
        PhotoUrls = [];
        this.Name = Name;
        this.Year = Year;
        this.Location = Location;
        this.MileAge = MileAge;
        this.Odometr = Odometr;
        this.GasType = GasType;   
    }
}