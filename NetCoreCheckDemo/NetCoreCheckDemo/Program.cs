using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetStandart.Calculator;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

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

                    config.AddEnvironmentVariables("DOTNETCORE_");

                    var a = new[] { "SomeValue=321", "ValueTest=4" };
                    config.AddCommandLine(a);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions<AppSettings>();
                    services.PostConfigureAll<TestSettings>(a =>
                    {
                        a.TestAge = 15;
                    });
                    services.Configure<TestSettings>("Test2", hostContext.Configuration.GetSection("TestSettings:Test2"));

                    services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<AppSettings>, AppConfigValidation>());
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                    services.Configure<DefaultStudent>(hostContext.Configuration.GetSection("DefaultStudent"));
                    services.AddScoped<IHostedService, HostService>();
                    services.AddScoped<ICalendar, Calendar>();
                    services.AddLogging(configure => configure.AddConsole());
                });

            await builder.RunConsoleAsync();
        }
    }
}