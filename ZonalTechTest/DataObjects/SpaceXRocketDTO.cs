using System.Text.Json.Serialization;

namespace ZonalTechTest.DataObjects
{
    public class SpaceXRocketDTO
    {
        [JsonPropertyName("rocket_id")]
        public string RocketId { get; set; } = null!;

        [JsonPropertyName("rocket_name")]
        public string RocketName { get; set; } = null!;

        [JsonPropertyName("rocket_type")]
        public string RocketType { get; set; } = null!;
    }
}
