using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Xunit;
using ZonalTechTest.Application;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Tests.Mocks;
using ZonalTechTest.Entities;

namespace ZonalTechTest.Tests
{
    public class RocketBLTests
    {
        private readonly IMapper _mapper;
        private readonly ISpaceXAPI _spaceXAPI;
        private readonly RocketBL _sut;

        public RocketBLTests()
        {
            _mapper = Substitute.For<IMapper>();
            _spaceXAPI = Substitute.For<ISpaceXAPI>();
            _sut = new RocketBL(_mapper, _spaceXAPI);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetRocketAsync_ShouldNotCallApi_WhenRocketIdIsEmpty(string rocketId)
        {
            _spaceXAPI.GetRocketDataAsync(Arg.Any<string>()).Returns(Task.FromResult<SpaceXRocketDTO?>(null));

            var rocketDto = await _sut.GetRocketAsync(rocketId);

            await _spaceXAPI.DidNotReceive().GetRocketDataAsync(rocketId);
            Assert.Null(rocketDto);
        }

        [Fact]
        public async Task GetRocketAsync_ShouldReturnNull_WhenSpaceXAPIReturnsNull()
        {
            var rocketId = "noSuchRocket";
            _spaceXAPI.GetRocketDataAsync(Arg.Any<string>()).Returns(Task.FromResult<SpaceXRocketDTO?>(null));

            var rocketDto = await _sut.GetRocketAsync(rocketId);

            await _spaceXAPI.Received().GetRocketDataAsync(rocketId);
            Assert.Null(rocketDto);
        }

        [Fact]
        public async Task GetRocketAsync_ShouldReturnRocketDto_WhenSpaceXAPIReturnsDto()
        {
            var spaceXRocketDto = new SpaceXRocketDTO { RocketName = "falcon1" };
            var rocketDto = new RocketDTO { RocketName = spaceXRocketDto.RocketName };
            _mapper.Map<RocketDTO>(spaceXRocketDto).Returns(rocketDto);
            _spaceXAPI.GetRocketDataAsync(spaceXRocketDto.RocketName).Returns(Task.FromResult<SpaceXRocketDTO?>(spaceXRocketDto));

            var result = await _sut.GetRocketAsync(spaceXRocketDto.RocketName);

            result.Should().BeEquivalentTo<RocketDTO>(rocketDto);
            await _spaceXAPI.Received().GetRocketDataAsync(spaceXRocketDto.RocketName);
        }

        [Fact]
        public async Task GetAllRocketsAsync_ShouldReturnEmptyList_WhenSpaceXAPIReturnsNull()
        {
            _spaceXAPI.GetAllRocketDataAsync().Returns(Task.FromResult<IEnumerable<SpaceXRocketDTO>?>(null));

            var allRockets = await _sut.GetAllRocketsAsync();

            await _spaceXAPI.Received().GetAllRocketDataAsync();
            Assert.True(!allRockets.Any());
        }

        [Fact]
        public async Task GetAllRocketsAsync_ShouldReturnRockets_WhenSpaceXAPIReturnsRockets()
        {
            var spaceXRocketDto1 = new SpaceXRocketDTO { RocketId = "falconheavy" };
            var spaceXRocketDto2 = new SpaceXRocketDTO { RocketType = "rocket" };
            var rocketDto1 = new RocketDTO { RocketId = spaceXRocketDto1.RocketId };
            var rocketDto2 = new RocketDTO { RocketType = spaceXRocketDto1.RocketId };
            IEnumerable<SpaceXRocketDTO> spaceXRockets = new[] { spaceXRocketDto1, spaceXRocketDto2 };
            _mapper.Map<IEnumerable<SpaceXRocketDTO>, IEnumerable<RocketDTO>>(spaceXRockets).Returns(new [] { rocketDto1, rocketDto2 });
            _spaceXAPI.GetAllRocketDataAsync().Returns(Task.FromResult<IEnumerable<SpaceXRocketDTO>?>(spaceXRockets));

            var allRockets = await _sut.GetAllRocketsAsync();

            allRockets.Should().BeEquivalentTo<RocketDTO>(new[] { rocketDto1, rocketDto2 });
            await _spaceXAPI.Received().GetAllRocketDataAsync();
        }
    }
}