using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Fact]
        public async void GetRocket_ShouldCallGetRocketAsync_WithRocketId()
        {
            var rocketId = "anything";
            await _sut.GetRocket(rocketId);

            await _rocketBl.Received().GetRocketAsync(rocketId);
        }

        [Fact]
        public async void GetAllRocketsAsync_ShouldCallGetRocketAsync_WithRocketId()
        {
            await _sut.GetAllRockets();

            await _rocketBl.Received().GetAllRocketsAsync();
        }
    }
}
