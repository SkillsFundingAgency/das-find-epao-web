using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SFA.DAS.FindEpao.Domain.Configuration;
using SFA.DAS.FindEpao.Domain.Exceptions;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Infrastructure.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly FindEpaoApi _config;

        public ApiClient (
            HttpClient httpClient, 
            IOptions<FindEpaoApi> options)
        {
            _httpClient = httpClient;
            _config = options.Value;
            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        }

        public async Task<TResponse> Get<TResponse>(IGetApiRequest request)
        {
            AddHeaders();

            var response = await _httpClient.GetAsync(request.GetUrl).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException<TResponse>();
            }
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<TResponse>(json);
        }

        public async Task<int> Ping()
        {
            AddHeaders();

            var result = await _httpClient.GetAsync($"{_config.PingUrl}ping");
            
            return (int)result.StatusCode;
        }

        private void AddHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.Key);
            _httpClient.DefaultRequestHeaders.Add("X-Version", "1");
        }
    }
}
