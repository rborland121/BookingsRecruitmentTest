using System.Text.Json.Serialization;

namespace ZonalTechTest.DataObjects
{
    public class SpaceXLaunchDTO
    {
        [JsonPropertyName("flight_number")]
        public int FlightNumber { get; set; }

        [JsonPropertyName("mission_name")]
        public string MissionName { get; set; } = null!;

        [JsonPropertyName("mission_id")]
        public string[] MissionId { get; set; } = null!;

        [JsonPropertyName("launch_year")]
        public string LaunchYear { get; set; } = null!;

        [JsonPropertyName("launch_date_unix")]
        public int LaunchDateUnix { get; set; }

        [JsonPropertyName("launch_date_utc")]
        public DateTime LaunchDateUTC { get; set; }

        [JsonPropertyName("launch_date_local")]
        public DateTime LaunchDateLocal { get; set; }

        [JsonPropertyName("rocket")]
        public SpaceXRocketDTO Rocket { get; set; } = null!;
    }
}
