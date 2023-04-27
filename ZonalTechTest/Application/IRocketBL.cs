using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Application
{
    public interface IRocketBL
    {
        Task<RocketDTO?> GetRocketAsync(string rocketId);

        Task<IEnumerable<RocketDTO>> GetAllRocketsAsync();
    }
}
