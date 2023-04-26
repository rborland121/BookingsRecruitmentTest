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

        public async void AddLaunchAsync(int launchId)
        {
            var launchData = await _api.GetLaunchDataAsync(launchId);

            if (launchData is null) { return; }

            var launchEntity = _mapper.Map<Launch>(launchData);
            var rocketEntity = _mapper.Map<Rocket>(launchData.Rocket);

            await _launchCommandRepo.AddLaunchAsync(launchEntity);

            await _rocketCommandRepo.AddRocketAsync(rocketEntity);
        }

        public async Task<IEnumerable<LaunchDTO>> GetLaunchAsync(int flightNumber)
        {
            if (flightNumber == 0) return Enumerable.Empty<LaunchDTO>();

            return await _launchQueryRepo.GetLaunchAsync(flightNumber);
        }
    }
}
