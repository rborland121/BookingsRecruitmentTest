using ZonalTechTest.Entities;

namespace ZonalTechTest.Repository;

public interface ILaunchCommandRepository
{
    Task<bool> AddLaunchAsync(Launch launch);
}
