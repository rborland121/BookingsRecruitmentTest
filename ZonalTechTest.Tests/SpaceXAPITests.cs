using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using ZonalTechTest.Application;
using ZonalTechTest.DataObjects;
using ZonalTechTest.Tests.Mocks;

namespace ZonalTechTest.Tests
{
    public class SpaceXAPITests
    {
        [Fact]
        public async Task GetLaunchDataAsync_ShouldReturnSpaceXRocketDto_WhenApiCallIsSuccessful()
        {
            var mockMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK);
            var flightNumber = 1;
            mockMessageHandler.ResponseJsonContent = JsonContent.Create<SpaceXLaunchDTO>(new SpaceXLaunchDTO { FlightNumber = flightNumber });
            var sut = new SpaceXAPI(mockMessageHandler.MockHttpClientFactory);

            var spaceDto = await sut.GetLaunchDataAsync(flightNumber);

            spaceDto.Should().BeEquivalentTo<SpaceXLaunchDTO>(new SpaceXLaunchDTO { FlightNumber = flightNumber });
            Assert.True(mockMessageHandler.GetAsyncCalled);
            Assert.Equal($"{SpaceXAPI.BASE_URL}{SpaceXAPI.LAUNCH_ENDPOINT}/" + flightNumber, mockMessageHandler.GetAsyncPath);
        }

        [Fact]
        public async Task GetLaunchDataAsync_ShouldReturnNull_WhenApiCallIsNotOk()
        {
            var mockMessageHandler = new MockHttpMessageHandler(HttpStatusCode.NotFound);
            var flightNumber = 1;
            var sut = new SpaceXAPI(mockMessageHandler.MockHttpClientFactory);

            var spaceDto = await sut.GetLaunchDataAsync(flightNumber);

            Assert.Null(spaceDto);
            Assert.True(mockMessageHandler.GetAsyncCalled);
            Assert.Equal($"{SpaceXAPI.BASE_URL}{SpaceXAPI.LAUNCH_ENDPOINT}/" + flightNumber, mockMessageHandler.GetAsyncPath);
        }

        [Fact]
        public async Task GetRocketDataAsync_ShouldReturnSpaceXLaunchDto_WhenApiCallIsSuccessful()
        {
            var mockMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK);
            var rocketId = "falcon1";
            mockMessageHandler.ResponseJsonContent = JsonContent.Create<SpaceXRocketDTO>(new SpaceXRocketDTO { RocketId = rocketId });
            var sut = new SpaceXAPI(mockMessageHandler.MockHttpClientFactory);

            var spaceDto = await sut.GetRocketDataAsync(rocketId);

            spaceDto.Should().BeEquivalentTo<SpaceXRocketDTO>(new SpaceXRocketDTO { RocketId = rocketId });
            Assert.True(mockMessageHandler.GetAsyncCalled);
            Assert.Equal($"{SpaceXAPI.BASE_URL}{SpaceXAPI.ROCKET_ENDPOINT}/" + rocketId, mockMessageHandler.GetAsyncPath);
        }

        [Fact]
        public async Task GetRocketDataAsync_ShouldReturnNull_WhenApiCallIsNOtOk()
        {
            var mockMessageHandler = new MockHttpMessageHandler(HttpStatusCode.BadRequest);
            var rocketId = "super heavy";
            var sut = new SpaceXAPI(mockMessageHandler.MockHttpClientFactory);

            var spaceDto = await sut.GetRocketDataAsync(rocketId);

            Assert.Null(spaceDto);
            Assert.True(mockMessageHandler.GetAsyncCalled);
            Assert.Equal($"{SpaceXAPI.BASE_URL}{SpaceXAPI.ROCKET_ENDPOINT}/" + rocketId, mockMessageHandler.GetAsyncPath);
        }

        [Fact]
        public async Task GetAllRocketDataAsync_ShouldReturnSpaceXLaunchDtoList_WhenApiCallIsSuccessful()
        {
            var mockMessageHandler = new MockHttpMessageHandler(HttpStatusCode.OK);
            var spaceXRocketDto1 = new SpaceXRocketDTO { RocketId = "rocket1" };
            var spaceXRocketDto2 = new SpaceXRocketDTO { RocketId = "rocket2" };
            mockMessageHandler.ResponseJsonContent = JsonContent.Create<IEnumerable<SpaceXRocketDTO>>(new[] { spaceXRocketDto1, spaceXRocketDto2 });
            var sut = new SpaceXAPI(mockMessageHandler.MockHttpClientFactory);

            var spaceDto = await sut.GetAllRocketDataAsync();

            spaceDto.Should().BeEquivalentTo<SpaceXRocketDTO>(new [] { spaceXRocketDto1, spaceXRocketDto2 });
            Assert.True(mockMessageHandler.GetAsyncCalled);
            Assert.Equal($"{SpaceXAPI.BASE_URL}{SpaceXAPI.ROCKET_ENDPOINT}", mockMessageHandler.GetAsyncPath);
        }

        [Fact]
        public async Task GetAllRocketDataAsync_ShouldReturnNull_WhenApiCallIsNotOk()
        {
            var mockMessageHandler = new MockHttpMessageHandler(HttpStatusCode.BadGateway);
            var spaceXRocketDto1 = new SpaceXRocketDTO { RocketId = "rocket1" };
            var spaceXRocketDto2 = new SpaceXRocketDTO { RocketId = "rocket2" };
            mockMessageHandler.ResponseJsonContent = JsonContent.Create<IEnumerable<SpaceXRocketDTO>>(new[] { spaceXRocketDto1, spaceXRocketDto2 });
            var sut = new SpaceXAPI(mockMessageHandler.MockHttpClientFactory);

            var spaceDto = await sut.GetAllRocketDataAsync();

            Assert.Null(spaceDto);
            Assert.True(mockMessageHandler.GetAsyncCalled);
            Assert.Equal($"{SpaceXAPI.BASE_URL}{SpaceXAPI.ROCKET_ENDPOINT}", mockMessageHandler.GetAsyncPath);
        }
    }
}
