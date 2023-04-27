using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using ZonalTechTest.Application;
using ZonalTechTest.Controllers;

namespace ZonalTechTest.Tests
{
    public class RocketControllerTests
    {
        private readonly IRocketBL _rocketBl;
        private readonly RocketController _sut;

        public RocketControllerTests()
        {
            _rocketBl = Substitute.For<IRocketBL>();
            _sut = new RocketController(_rocketBl);
        }

        [Theory]
        [InlineData("anything")]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetRocket_ShouldCallBusinessLayer_WithAnyRocketId(string rocketId)
        {
            await _sut.GetRocket(rocketId);

            await _rocketBl.Received().GetRocketAsync(rocketId);
        }

        [Fact]
        public async Task GetAllRocketsAsync_ShouldCallBusinessLayer()
        {
            await _sut.GetAllRockets();

            await _rocketBl.Received().GetAllRocketsAsync();
        }
    }
}
