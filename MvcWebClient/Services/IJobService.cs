using MvcWebClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcWebClient.Services
{
    public interface IJobService
    {
        Task<Job> GetJob(int jobId);
        Task<IEnumerable<Job>> GetJobs();
    }
}
