using CarDealershipApi.Models;

namespace CarDealershipApi.Dtos
{
    public class CarDamageDto
    {
        public DamageType Type { get; set; }
        public string? Description { get; set; }
        public SeverityLevel Severity { get; set; }

    }
}
