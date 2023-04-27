namespace ZonalTechTest.DataObjects
{
    public class LaunchDTO
    {
        public int FlightNumber { get; set; }
        public string MissionName { get; set; } = null!;
        public string LaunchYear { get; set; } = null!;
        public DateTime LaunchDateUTC { get; set; }
    }
}
