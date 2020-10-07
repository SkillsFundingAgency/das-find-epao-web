using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Configuration;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Infrastructure.Api;
using SFA.DAS.FindEpao.Infrastructure.HealthCheck;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Infrastructure.UnitTests.Api
{
    public class WhenCallingPing
    {
        [Test, MoqAutoData]
        public async Task Then_The_Ping_Endpoint_Is_Called_For_OuterApi(
            [Frozen] Mock<IApiClient> client,
            HealthCheckContext healthCheckContext,
            FindEpaoOuterApiHealthCheck healthCheck)
        {
            //Act
            await healthCheck.CheckHealthAsync(healthCheckContext, CancellationToken.None);
            //Assert
            client.Verify(x => x.Ping(), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task Then_The_Endpoint_Is_Called_And_The_Status_is_Returned(
            FindEpaoApi config)
        {
            //Arrange
            config.PingUrl = "https://test.local/";
            config.BaseUrl = "http://valid-url/";
            var configMock = new Mock<IOptions<FindEpaoApi>>();
            configMock.Setup(x => x.Value).Returns(config);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, $"{config.PingUrl}ping", config.Key);
            var client = new HttpClient(httpMessageHandler.Object);
            var apiClient = new ApiClient(client, configMock.Object);
            //Act
            var actual = await apiClient.Ping();
            //Assert
            actual.Should().Be((int)HttpStatusCode.Accepted);
        }

        [Test, MoqAutoData]
        public async Task Then_If_It_Is_Successful_200_Is_Returned(
            [Frozen] Mock<IApiClient> client,
            HealthCheckContext healthCheckContext,
            FindEpaoOuterApiHealthCheck healthCheck)
        {
            //Arrange
            client.Setup(x => x.Ping())
                .ReturnsAsync(200);
            //Act
            var actual = await healthCheck.CheckHealthAsync(healthCheckContext, CancellationToken.None);
            //Assert
            Assert.AreEqual(HealthStatus.Healthy, actual.Status);
        }

        [Test, MoqAutoData]
        public async Task Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown(
            [Frozen] Mock<IApiClient> client,
            HealthCheckContext healthCheckContext,
            FindEpaoOuterApiHealthCheck healthCheck)
        {
            //Arrange
            client.Setup(x => x.Ping())
                .ReturnsAsync(404);
            //Act
            var actual = await healthCheck.CheckHealthAsync(healthCheckContext, CancellationToken.None);
            //Assert
            Assert.AreEqual(HealthStatus.Unhealthy, actual.Status);
        }
    }
}
