using JobsApi.Models;
using System.Linq;

namespace JobsApi.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(JobsContext context)
        {
            context.Database.EnsureCreated();
            if (context.Jobs.Any())
            {
                return;
            }
            var jobs = new Job[]
            {
                new Job{ Title ="Senior Software Engineer", Description ="we are seeking", Location="Islamabad", Company="Synergy-IT", PostedDate=System.DateTime.Now },
                new Job{ Title ="Dotnet Developer", Description ="we are seeking", Location="Islamabad", Company="Synergy-IT", PostedDate=System.DateTime.Now },
                new Job{ Title ="Full-Stack Developer", Description ="we are seeking", Location="Islamabad", Company="Synergy-IT", PostedDate=System.DateTime.Now },
            };
            context.Jobs.AddRange(jobs);
            context.SaveChanges();
        }
    }
}
