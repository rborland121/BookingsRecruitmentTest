using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using ZonalTechTest.Application;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Entities;
using ZonalTechTest.Repository;
using ZonalTechTest.Tests.Mocks;

namespace ZonalTechTest.Tests
{
    public class LaunchBLTests
    {
        private readonly LaunchBL _sut;
        private readonly IMapper _mapper;
        private readonly ILaunchCommandRepository _launchCommandRepo;
        private readonly IRocketCommandRepository _rocketCommandRepo;
        private readonly ILaunchQueryRepository _launchQueryRepo;
        private readonly ISpaceXAPI _spaceXAPI;

        public LaunchBLTests()
        {
            _mapper = Substitute.For<IMapper>();
            _launchCommandRepo = Substitute.For<ILaunchCommandRepository>();
            _rocketCommandRepo = Substitute.For<IRocketCommandRepository>();
            _launchQueryRepo = Substitute.For<ILaunchQueryRepository>();
            _spaceXAPI = Substitute.For<ISpaceXAPI>();
            _sut = new LaunchBL(_mapper, _launchCommandRepo, _rocketCommandRepo, _launchQueryRepo, _spaceXAPI);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(8)]
        public async Task AddLaunchAsync_ShouldReturnFalse_WhenSpaceXApiDataNotFound(int flightNumber)
        {
            _spaceXAPI.GetLaunchDataAsync(flightNumber).Returns(Task.FromResult<SpaceXLaunchDTO?>(null!));

            var result = await _sut.AddLaunchAsync(flightNumber);

            await _spaceXAPI.Received().GetLaunchDataAsync(flightNumber);
            await _rocketCommandRepo.DidNotReceive().AddRocketAsync(Arg.Any<Rocket>());
            await _launchCommandRepo.DidNotReceive().AddLaunchAsync(Arg.Any<Launch>());
            Assert.False(result);
        }

        [Fact]
        public async Task AddLaunchAsync_ShouldCallCommandRepos_WhenSpaceXLaunchFound()
        {
            var flightNumber = 3;
            var spaceXDto = new SpaceXLaunchDTO();
            var launch = new Launch();
            var rocket = new Rocket();
            _spaceXAPI.GetLaunchDataAsync(flightNumber).Returns(Task.FromResult<SpaceXLaunchDTO?>(spaceXDto));
            _mapper.Map<Launch>(spaceXDto).Returns(launch);
            _mapper.Map<Rocket>(spaceXDto.Rocket).Returns(rocket);

            var result = await _sut.AddLaunchAsync(flightNumber);

            await _launchCommandRepo.Received().AddLaunchAsync(launch);
            await _rocketCommandRepo.Received().AddRocketAsync(rocket);
            Assert.True(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public async Task GetLaunchAsync_ShouldNotCallRepo_WhenFlightNoIsLessThanOne(int flightNumber)
        {
            var result = await _sut.GetLaunchAsync(flightNumber);

            await _launchQueryRepo.DidNotReceive().GetLaunchAsync(flightNumber);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(9)]
        public async Task GetLaunchAsync_ShouldCallRepo_WhenFlightNoIsGreaterThanZero(int flightNumber)
        {
            var launchDto = new LaunchDTO();
            _launchQueryRepo.GetLaunchAsync(flightNumber).Returns(launchDto);
            var result = await _sut.GetLaunchAsync(flightNumber);

            await _launchQueryRepo.Received().GetLaunchAsync(flightNumber);
            Assert.Equal(launchDto, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public async Task DeleteLaunchAsync_ShouldNotCallRepo_WhenFlightNoIsLessThanOne(int flightNumber)
        {
            var result = await _sut.DeleteLaunchAsync(flightNumber);

            await _launchCommandRepo.DidNotReceive().DeleteLaunchAsync(Arg.Any<int>());
            Assert.False(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task DeleteLaunchAsync_ShouldCallRepo_WhenFlightNoIsGreaterThanZero(int flightNumber)
        {
            _launchCommandRepo.DeleteLaunchAsync(flightNumber).Returns(true);
            var result = await _sut.DeleteLaunchAsync(flightNumber);

            await _launchCommandRepo.Received().DeleteLaunchAsync(flightNumber);
            Assert.True(result);
        }

        [Fact]
        public async Task GetLaunchesAsync_ShouldCallRepo()
        {
            var launchDto = new LaunchDTO();
            _launchQueryRepo.GetLaunchesAsync().Returns(new [] { launchDto });
            var result = await _sut.GetLaunchesAsync();

            await _launchQueryRepo.Received().GetLaunchesAsync();
            result.Should().BeEquivalentTo<LaunchDTO>(new[] { launchDto });
        }
    }
}