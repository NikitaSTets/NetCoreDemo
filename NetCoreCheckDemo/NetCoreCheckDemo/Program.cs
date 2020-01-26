using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetStandart.Calculator;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetCoreCheckDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables("DOTNETCORE_");
                    config.AddJsonFile("hostsettings.json", false, true);
                    config.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();

                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                    services.Configure<DefaultStudent>(hostContext.Configuration.GetSection("DefaultStudent"));
                    services.AddHostedService<HostService>();
                    services.AddScoped<ICalendar, Calendar>();
                    services.AddLogging(configure => configure.AddConsole());
                });

            await builder.RunConsoleAsync();
        }
    }
}