using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Infrastructure.HealthCheck
{
    public class FindEpaoOuterApiHealthCheck : IHealthCheck
    {
        private const string HealthCheckResultDescription = "FindEpao Outer Api check";

        private readonly IApiClient _apiClient;
        private readonly ILogger<FindEpaoOuterApiHealthCheck> _logger;

        public FindEpaoOuterApiHealthCheck(IApiClient apiClient, ILogger<FindEpaoOuterApiHealthCheck> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Pinging FindEpao Outer API");

            var timer = Stopwatch.StartNew();
            var response = await _apiClient.Ping();
            timer.Stop();
            
            if(response == 200)
            {
                var durationString = timer.Elapsed.ToHumanReadableString();

                _logger.LogInformation($"FindEpao Outer API ping successful and took {durationString}");

                return HealthCheckResult.Healthy(HealthCheckResultDescription,
                    new Dictionary<string, object> { { "Duration", durationString } });
            }

            _logger.LogWarning($"FindEpao Outer API ping failed : [Code: {response}]");
            return HealthCheckResult.Unhealthy(HealthCheckResultDescription);
            
        }
    }
}
