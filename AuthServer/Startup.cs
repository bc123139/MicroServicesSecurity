using AuthServer.Config;
using AuthServer.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace AuthServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OAuthIdentity")));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserContext>()
                .AddDefaultTokenProviders();
            var builder = services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim= true;
            }).AddTestUsers(MemoryConfig.TestUsers()).AddConfigurationStore(opt =>
            {
                opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("OAuth"),
                sql => sql.MigrationsAssembly(migrationAssembly));
            }).AddOperationalStore(opt =>
            {
                opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("OAuth"),
                sql => sql.MigrationsAssembly(migrationAssembly));
            })
            .AddAspNetIdentity<User>();
            builder.AddDeveloperSigningCredential();
            /*
             Migrations:
            Add-Migration InitialPersistedGrantMigration -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
            Add-Migration InitialConfigurationMigration -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
            --Identity tables:
            Add-Migration CreateIdentityTables -c UserContext
            Update-Database -Context UserContext
            Add-Migration AddRolesToDb -Context UserContext
            Update-Database -Context UserContext
             */
            services.AddControllersWithViews();
            //for in memory idenity server--
            //services.AddIdentityServer()
            //    .AddInMemoryIdentityResources(MemoryConfig.IdentityResourecs())
            //    .AddInMemoryClients(MemoryConfig.Clients())
            //    .AddTestUsers(MemoryConfig.TestUsers())
            //    .AddInMemoryApiScopes(MemoryConfig.ApiScopes())
            //    .AddInMemoryApiResources(MemoryConfig.ApiResources())
            //    .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            
        }
    }
}
