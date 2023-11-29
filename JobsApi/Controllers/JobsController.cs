using JobsApi.Data;
using JobsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class JobsController : ControllerBase
    {
        private readonly JobsContext _jobsContext;

        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger, JobsContext jobsContext)
        {
            _logger = logger;
            _jobsContext = jobsContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Job>> Get()
        {
            var claims = User.Claims;
            return await _jobsContext.Jobs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Job> Get(int id)
        {
            return await _jobsContext.Jobs.FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
