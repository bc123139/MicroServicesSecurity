using AuthServer.Config;
using AuthServer.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace AuthServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger=new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime",LogEventLevel.Information)
                .MinimumLevel.Override("System",LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication",LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate:"[{TimeStamp:HHM:mm:ss} {Level}] {SourceContext}")
                .CreateLogger();
            var builder = CreateHostBuilder(args).Build();
            var config = builder.Services.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("OAuthIdentity");
            WebClientConfig.ClientUrl = config["WebClientUrl"];
            SeedUserData.InsertSeedData(connectionString);
            builder.MigrateDatabase().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
