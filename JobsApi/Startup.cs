using JobsApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace JobsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.Authority = "https://localhost:5011";//Auth Server
                    opt.Audience = "jobsApi";
                });
            services.AddControllers();
            AddDbContext(services);
        }
        public void AddDbContext(IServiceCollection services)
        {
            var debugLogging = new Action<DbContextOptionsBuilder>(opt =>
            {
                opt.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                }));
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
            });
            services.AddDbContext<JobsContext>(opt =>
            {
                var connectionString= Configuration.GetConnectionString("sqldb-job") ?? "sqldb";
                opt.UseSqlServer(connectionString,opt=>opt.EnableRetryOnFailure(5));
                debugLogging(opt);
            },ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            TryRunMigrationsAndSeedDatabase(app);
        }
        private void TryRunMigrationsAndSeedDatabase(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<JobsContext>();
                dbContext.Database.Migrate();
                DatabaseInitializer.Initialize(dbContext);
            }
        }

    }
}
