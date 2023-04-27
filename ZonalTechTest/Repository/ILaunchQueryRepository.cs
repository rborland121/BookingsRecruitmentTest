using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Repository;

public interface ILaunchQueryRepository
{
    Task<LaunchDTO?> GetLaunchAsync(int flightNumber);
    Task<IEnumerable<LaunchDTO>> GetLaunchesAsync();
}
