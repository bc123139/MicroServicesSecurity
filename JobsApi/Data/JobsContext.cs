using JobsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Data
{
    public class JobsContext :DbContext
    {
        public JobsContext(DbContextOptions<JobsContext> options):base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
    }
}
