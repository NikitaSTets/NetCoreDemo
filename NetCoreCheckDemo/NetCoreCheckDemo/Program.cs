using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetStandart.Calculator;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace NetCoreCheckDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddEnvironmentVariables("DOTNETCORE_");
                    config.AddJsonFile("hostsettings.json", false, true);
                    config.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddEnvironmentVariables("DOTNETCORE_");

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);


                    var a = new[] { "SomeValue=321", "ValueTest=4" };
                    config.AddCommandLine(a);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole(options =>
                    {
                        options.TimestampFormat = "[HH:mm] ";
                        options.DisableColors = false;
                        options.Format = ConsoleLoggerFormat.Systemd;
                    });

                    //custom logging provider
                    //logging.Services.AddSingleton<ILoggerProvider, JsonLoggerProvider>();

                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.PostConfigureAll<TestSettings>(a =>
                    {
                        a.TestAge = 15;
                    });
                    services.Configure<TestSettings>("Test2", hostContext.Configuration.GetSection("TestSettings:Test2"));

                    services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<AppSettings>, AppConfigValidation>());
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                    services.Configure<DefaultStudent>(hostContext.Configuration.GetSection("DefaultStudent"));
                    services.AddHostedService<HostService>();
                    services.AddSingleton<ICalendar, Calendar>();
                });

            await builder.RunConsoleAsync();
        }
    }
}