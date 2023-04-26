using AutoMapper;
using ZonalTechTest.Helpers;

namespace ZonalTechTest.Tests.Mocks;

public class TestAutoMapper
{
    public static IMapper Mapper { get; private set; }

    static TestAutoMapper()
    {
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfiles()); });
        var mapper = mappingConfig.CreateMapper();
        Mapper = mapper;
    }
}