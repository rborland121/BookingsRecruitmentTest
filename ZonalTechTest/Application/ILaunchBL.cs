using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Application;

public interface ILaunchBL
{
    Task<bool> AddLaunchAsync(int flightNumber);
    Task<LaunchDTO?> GetLaunchAsync(int flightNumber);
    Task<bool> DeleteLaunchAsync(int flightNumber);
    Task<IEnumerable<LaunchDTO>> GetLaunchesAsync();
}
