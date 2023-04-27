using AutoMapper;
using ZonalTechTest.DataObjects;

namespace ZonalTechTest.Application
{
    public class RocketBL : IRocketBL
    {
        private readonly ISpaceXAPI _api;
        private readonly IMapper _mapper;

        public RocketBL(IMapper mapper, ISpaceXAPI api)
        {
            _mapper = mapper;
            _api = api;
        }

        public async Task<RocketDTO?> GetRocketAsync(string rocketId)
        {
            if (string.IsNullOrEmpty(rocketId))
            {
                return null;
            }

            var spaceXRocketDto = await _api.GetRocketDataAsync(rocketId);
            var rocket = _mapper.Map<RocketDTO>(spaceXRocketDto);

            return rocket;
        }

        public async Task<IEnumerable<RocketDTO>> GetAllRocketsAsync()
        {
            var spaceXRocketDtoData = await _api.GetAllRocketDataAsync();
            var rockets = _mapper.Map<IEnumerable<SpaceXRocketDTO>, IEnumerable<RocketDTO>>(spaceXRocketDtoData!);

            return rockets;
        }
    }
}
