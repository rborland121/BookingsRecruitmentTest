using AutoMapper;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Entities;

namespace ZonalTechTest.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SpaceXLaunchDTO, Launch>()
                .ForMember(dest => dest.RocketId, opt => opt.MapFrom(src => src.Rocket.RocketId));
            CreateMap<SpaceXRocketDTO, Rocket>();
            CreateMap<Launch, LaunchDTO>();
            CreateMap<SpaceXRocketDTO, RocketDTO>();
        }
    }
}
