using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using ZonalTechTest.Application;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Tests.Mocks;

namespace ZonalTechTest.Tests
{
    public class RocketBLTests
    {
        private readonly ISpaceXAPI _spaceXAPI;
        private readonly RocketBL _sut;

        public RocketBLTests()
        {
            _spaceXAPI = Substitute.For<ISpaceXAPI>();
            _sut = new RocketBL(TestAutoMapper.Mapper, _spaceXAPI);
        }

        [Fact]
        public async void GetRocketAsync_ShouldReturnNull_WhenSpaceXAPIReturnsNull()
        {
            _spaceXAPI.GetSpaceXRocketDataAsync(Arg.Any<string>()).Returns(Task.FromResult<SpaceXRocketDTO?>(null));

            var rocketDto = await _sut.GetRocketAsync("noSuchRocket");

            Assert.Null(rocketDto);
            await _spaceXAPI.Received().GetSpaceXRocketDataAsync("noSuchRocket");
        }

        [Fact]
        public async void GetRocketAsync_ShouldReturnRocketDto_WhenSpaceXAPIReturnsDto()
        {
            var spaceXRocketDto = new SpaceXRocketDTO { RocketName = "falcon1" };
            _spaceXAPI.GetSpaceXRocketDataAsync(spaceXRocketDto.RocketName).Returns(Task.FromResult<SpaceXRocketDTO?>(spaceXRocketDto));

            var rocketDto = await _sut.GetRocketAsync(spaceXRocketDto.RocketName);

            rocketDto.Should().BeEquivalentTo<RocketDTO>(new RocketDTO { RocketName = spaceXRocketDto.RocketName });
            await _spaceXAPI.Received().GetSpaceXRocketDataAsync(spaceXRocketDto.RocketName);
        }

        [Fact]
        public async void GetAllRocketsAsync_ShouldReturnEmpty_WhenSpaceXAPIReturnsNull()
        {
            _spaceXAPI.GetAllSpaceXRocketDataAsync().Returns(Task.FromResult<IEnumerable<SpaceXRocketDTO>?>(null));

            var allRockets = await _sut.GetAllRocketsAsync();

            Assert.True(!allRockets.Any());
            await _spaceXAPI.Received().GetAllSpaceXRocketDataAsync();
        }

        [Fact]
        public async void GetAllRocketsAsync_ShouldReturnRockets_WhenSpaceXAPIReturnsRockets()
        {
            var spaceXRocketDto1 = new SpaceXRocketDTO { RocketId = "rocket1" };
            var spaceXRocketDto2 = new SpaceXRocketDTO { RocketId = "rocket2" };
            IEnumerable<SpaceXRocketDTO> spaceXRockets = new[] { spaceXRocketDto1, spaceXRocketDto2 };
            _spaceXAPI.GetAllSpaceXRocketDataAsync().Returns(Task.FromResult<IEnumerable<SpaceXRocketDTO>?>(spaceXRockets));

            var allRockets = await _sut.GetAllRocketsAsync();

            allRockets.Should().BeEquivalentTo<RocketDTO>(new []
            {
                new RocketDTO {RocketId = spaceXRocketDto1.RocketId}, new RocketDTO { RocketId = spaceXRocketDto2.RocketId }
            });
            await _spaceXAPI.Received().GetAllSpaceXRocketDataAsync();
        }
    }
}