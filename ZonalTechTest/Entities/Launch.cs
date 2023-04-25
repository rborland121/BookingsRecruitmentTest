namespace ZonalTechTest.Entities;

public class Launch
{
    public int FlightNumber { get; set; }
    public string MissionName { get; set; }
    public string LaunchYear { get; set; }
    public DateTime LaunchDateUTC { get; set; }
    public string RocketId { get; set; }
}
