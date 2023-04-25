using ZonalTechTest.Entities;

namespace ZonalTechTest.Repository;

public interface IRocketCommandRepository
{
    Task<bool> AddRocketAsync(Rocket rocket);
}
