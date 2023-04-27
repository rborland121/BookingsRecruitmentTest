using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Application;

public interface ISpaceXAPI
{
    Task<SpaceXLaunchDTO?> GetLaunchDataAsync(int flightNumber = 0);
    Task<SpaceXRocketDTO?> GetRocketDataAsync(string rocketId);
    Task<IEnumerable<SpaceXRocketDTO>?> GetAllRocketDataAsync();
}