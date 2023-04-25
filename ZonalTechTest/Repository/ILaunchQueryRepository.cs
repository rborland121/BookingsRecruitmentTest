using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Repository;

public interface ILaunchQueryRepository
{
    Task<IEnumerable<LaunchDTO>> GetLaunchAsync(int flightNumber);
}
