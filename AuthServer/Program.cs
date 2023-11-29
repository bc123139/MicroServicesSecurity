using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            CreateHostBuilder(args).Build().Run();
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
