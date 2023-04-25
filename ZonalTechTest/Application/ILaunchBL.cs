using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Application;

public interface ILaunchBL
{
    void AddLaunchAsync(int launchId);
    Task<IEnumerable<LaunchDTO>> GetLaunchAsync(int flightNumber);
}
