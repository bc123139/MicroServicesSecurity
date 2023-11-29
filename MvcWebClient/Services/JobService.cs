using Microsoft.Extensions.Options;
using MvcWebClient.Config;
using MvcWebClient.Http;
using MvcWebClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcWebClient.Services
{
    public class JobService : IJobService
    {
        private readonly IHttpClient _apiClient;
        private readonly ApiConfig _apiConfig;
        public JobService(IHttpClient apiClient, IOptionsMonitor<ApiConfig> apiConfig)
        {
            _apiClient = apiClient;
            _apiConfig = apiConfig.CurrentValue;
        }
        public async Task<Job> GetJob(int jobId)
        {
            var dataString = await _apiClient.GetStringAsync($"{_apiConfig.JobsApiUrl}/jobs/{jobId}");
            return JsonConvert.DeserializeObject<Job>(dataString);
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var dataString = await _apiClient.GetStringAsync($"{_apiConfig.JobsApiUrl}/jobs");
            return JsonConvert.DeserializeObject<IEnumerable<Job>>(dataString);
        }
    }
}
