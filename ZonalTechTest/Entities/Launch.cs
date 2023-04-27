namespace ZonalTechTest.Entities;

public class Launch
{
    public int FlightNumber { get; set; }
    public string MissionName { get; set; } = null!;
    public string LaunchYear { get; set; } = null!;
    public DateTime LaunchDateUTC { get; set; }
    public string RocketId { get; set; } = null!;
}
