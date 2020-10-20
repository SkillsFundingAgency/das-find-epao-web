using System.Threading.Tasks;
using SFA.DAS.FindEpao.Domain.Epaos;
using SFA.DAS.FindEpao.Domain.Epaos.Api;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Application.Epaos.Services
{
    public class EpaoService : IEpaoService
    {
        private readonly ICacheStorageService _cacheStorageService;
        private readonly IApiClient _apiClient;
        private const int CacheDurationInHours = 1;

        public EpaoService(
            ICacheStorageService cacheStorageService,
            IApiClient apiClient)
        {
            _cacheStorageService = cacheStorageService;
            _apiClient = apiClient;
        }

        public async Task<DeliveryAreaList> GetDeliveryAreas()
        {
            var cachedDeliveryAreas = await _cacheStorageService.RetrieveFromCache<DeliveryAreaList>(nameof(DeliveryAreaList));

            if (cachedDeliveryAreas != default)
            {
                return cachedDeliveryAreas;
            }

            var apiRequest = new GetDeliveryAreasApiRequest();
            var deliveryAreas = await _apiClient.Get<DeliveryAreaList>(apiRequest);

            await _cacheStorageService.SaveToCache(nameof(DeliveryAreaList), deliveryAreas, CacheDurationInHours);

            return deliveryAreas;
        }
    }
}
