using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.FindEpao.Domain.Configuration;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Infrastructure.Api;

namespace SFA.DAS.FindEpao.Infrastructure.UnitTests.Api
{
    public class WhenCallingGet
    {
        [Test, AutoData]
        public async Task Then_The_Endpoint_Is_Called_With_Authentication_Header_And_Data_Returned(
            List<string> testObject, 
            GetTestRequest getTestRequest,
            FindEpaoApi config)
        {
            //Arrange
            config.BaseUrl = "http://valid-url/";
            var configMock = new Mock<IOptions<FindEpaoApi>>();
            configMock.Setup(x => x.Value).Returns(config);
            
            var response = new HttpResponseMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(testObject)),
                StatusCode = HttpStatusCode.Accepted
            };
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, $"{config.BaseUrl}{getTestRequest.GetUrl}", config.Key);
            var client = new HttpClient(httpMessageHandler.Object);
            var apiClient = new ApiClient(client, configMock.Object);

            //Act
            var actual = await apiClient.Get<List<string>>(getTestRequest);
            
            //Assert
            actual.Should().BeEquivalentTo(testObject);
        }
        
        [Test, AutoData]
        public void Then_If_It_Is_Not_Successful_An_Exception_Is_Thrown(
            GetTestRequest getTestRequest,
            FindEpaoApi config)
        {
            //Arrange
            config.BaseUrl = "http://valid-url/";
            var configMock = new Mock<IOptions<FindEpaoApi>>();
            configMock.Setup(x => x.Value).Returns(config);
            var response = new HttpResponseMessage
            {
                Content = new StringContent(""),
                StatusCode = HttpStatusCode.BadRequest
            };
            
            var httpMessageHandler = MessageHandler.SetupMessageHandlerMock(response, $"{config.BaseUrl}{getTestRequest.GetUrl}", config.Key);
            var client = new HttpClient(httpMessageHandler.Object);
            var apiClient = new ApiClient(client, configMock.Object);
            
            //Act Assert
            Assert.ThrowsAsync<HttpRequestException>(() => apiClient.Get<List<string>>(getTestRequest));
            
        }

        public class GetTestRequest : IGetApiRequest
        {
            public string GetUrl => "test-url/get";
        }
    }
}
