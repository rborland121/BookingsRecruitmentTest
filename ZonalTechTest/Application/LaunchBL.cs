using AutoMapper;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Entities;
using ZonalTechTest.Repository;

namespace ZonalTechTest.Application
{
    public class LaunchBL : ILaunchBL
    {
        private readonly IMapper _mapper;
        private readonly ILaunchCommandRepository _launchCommandRepo;
        private readonly IRocketCommandRepository _rocketCommandRepo;
        private readonly ILaunchQueryRepository _launchQueryRepo;
        private readonly ISpaceXAPI _api;

        public LaunchBL(IMapper mapper, ILaunchCommandRepository launchCommandRepo,
            IRocketCommandRepository rocketCommandRepo, ILaunchQueryRepository launchQueryRepo, ISpaceXAPI api)
        {
            _mapper = mapper;
            _launchCommandRepo = launchCommandRepo;
            _rocketCommandRepo = rocketCommandRepo;
            _launchQueryRepo = launchQueryRepo;
            _api = api;
        }

        public async Task<bool> AddLaunchAsync(int flightNumber)
        {
            var launchData = await _api.GetLaunchDataAsync(flightNumber);

            if (launchData is null)
            {
                return false;
            }

            var launchEntity = _mapper.Map<Launch>(launchData);
            var rocketEntity = _mapper.Map<Rocket>(launchData.Rocket);

            await _rocketCommandRepo.AddRocketAsync(rocketEntity);
            await _launchCommandRepo.AddLaunchAsync(launchEntity);

            return true;
        }

        public async Task<LaunchDTO?> GetLaunchAsync(int flightNumber)
        {
            return flightNumber > 0 ? await _launchQueryRepo.GetLaunchAsync(flightNumber) : null;
        }

        public async Task<bool> DeleteLaunchAsync(int flightNumber)
        {
            return flightNumber > 0 && await _launchCommandRepo.DeleteLaunchAsync(flightNumber);
        }

        public async Task<IEnumerable<LaunchDTO>> GetLaunchesAsync()
        {
            return await _launchQueryRepo.GetLaunchesAsync();
        }
    }
}
