using System.Text.Json.Serialization;

namespace CarDealershipApi.Models
{
    public class CarDamage
    {
            public int Id { get; set; }
            public DamageType Type { get; set; }
            public string? Description { get; set; }
            public SeverityLevel Severity { get; set; }

        // one car advert to many Car Damages
        public int CarAdvertId { get; set; }

        [JsonIgnore]
        public CarAdvert CarAdvert { get; set; }
    }

    public enum SeverityLevel { Low, Medium, High }
    public enum DamageType
    {
        NORMAL_WEAR,
        HAIL,
        MINOR_DENT_SCRATCHES,
        ALL_OVER,
        BIOHAZARD_CHEMICAL,
        BURN,
        BURN_ENGINE,
        BURN_INTERIOR,
        DAMAGE_HISTORY,
        FRAME_DAMAGE,
        FRONT_END,
        MECHANICAL,
        MISSING_ALTERED_VIN,
        PARTIAL_REPAIR,
        REAR_END,
        REJECTED_REPAIR,
        REPLACED_VIN,
        ROLLOVER,
        SIDE,
        STRIPPED,
        TOP_ROOF,
        UNDERCARRIAGE,
        UNKNOWN,
        VANDALISM,
        WATER_FLOOD
    }
}