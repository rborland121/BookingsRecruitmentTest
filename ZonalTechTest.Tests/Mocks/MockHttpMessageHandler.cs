using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ZonalTechTest.DataObjects;
using NSubstitute;
using System;

namespace ZonalTechTest.Tests.Mocks
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private HttpStatusCode _responseStatusCode;

        public MockHttpMessageHandler(HttpStatusCode statusCode)
        {
            _responseStatusCode = statusCode;
            MockHttpClientFactory = Substitute.For<IHttpClientFactory>();
            var httpClient = new HttpClient(this) { BaseAddress = new Uri("https://localhost") };
            MockHttpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);
        }

        public IHttpClientFactory MockHttpClientFactory { get; set;  }

        public bool GetAsyncCalled { get; private set; }

        public string GetAsyncPath { get; private set; }

        public JsonContent? ResponseJsonContent { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            GetAsyncCalled = request.Method == HttpMethod.Get;
            var httpResponseMessage = new HttpResponseMessage(_responseStatusCode);
            if (GetAsyncCalled)
            {
                GetAsyncPath = request.RequestUri.OriginalString;
                httpResponseMessage.Content = ResponseJsonContent;
            }

            return Task.FromResult(httpResponseMessage);
        }
    }
}
