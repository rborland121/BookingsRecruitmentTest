using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Application;

public interface ISpaceXAPI
{
    Task<SpaceXLaunchDTO?> GetLaunchDataAsync(int launchId = 0);
    Task<SpaceXRocketDTO?> GetSpaceXRocketDataAsync(string rocketId);
    Task<IEnumerable<SpaceXRocketDTO>?> GetAllSpaceXRocketDataAsync();
}