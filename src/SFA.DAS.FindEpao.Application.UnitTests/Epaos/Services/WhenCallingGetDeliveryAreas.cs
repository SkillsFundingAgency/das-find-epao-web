using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Epaos.Services;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Domain.Epaos.Api;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Epaos.Services
{
    public class WhenCallingGetDeliveryAreas
    {
        [Test, MoqAutoData]
        public async Task And_Not_Cached_Then_Gets_DeliveryAreas_From_Api(
            DeliveryAreaList deliveryAreasFromApi,
            [Frozen] Mock<ICacheStorageService> mockCacheService,
            [Frozen] Mock<IApiClient> mockApiClient,
            EpaoService service)
        {
            mockCacheService
                .Setup(storageService => storageService.RetrieveFromCache<DeliveryAreaList>(nameof(DeliveryAreaList)))
                .ReturnsAsync((DeliveryAreaList) default);
            mockApiClient
                .Setup(client => client.Get<DeliveryAreaList>(It.IsAny<GetDeliveryAreasApiRequest>()))
                .ReturnsAsync(deliveryAreasFromApi);

            var result = await service.GetDeliveryAreas();

            result.DeliveryAreas.Should().BeEquivalentTo(deliveryAreasFromApi.DeliveryAreas);
            mockCacheService.Verify(storageService => storageService.SaveToCache(nameof(DeliveryAreaList), deliveryAreasFromApi, 1), 
                Times.Once);
        }

        [Test, MoqAutoData]
        public async Task And_Is_Cached_Then_Gets_DeliveryAreas_From_Cache(
            DeliveryAreaList deliveryAreasFromCache,
            DeliveryAreaList deliveryAreasFromApi,
            [Frozen] Mock<ICacheStorageService> mockCacheService,
            [Frozen] Mock<IApiClient> mockApiClient,
            EpaoService service)
        {
            mockCacheService
                .Setup(storageService => storageService.RetrieveFromCache<DeliveryAreaList>(nameof(DeliveryAreaList)))
                .ReturnsAsync(deliveryAreasFromCache);

            var result = await service.GetDeliveryAreas();

            result.DeliveryAreas.Should().BeEquivalentTo(deliveryAreasFromCache.DeliveryAreas);
            mockApiClient.Verify(client => client.Get<DeliveryAreaList>(It.IsAny<GetDeliveryAreasApiRequest>()), 
                Times.Never);
        }
    }
}
