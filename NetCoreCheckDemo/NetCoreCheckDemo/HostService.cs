using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetStandart.Calculator;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetCoreCheckDemo
{
    public class HostService : BackgroundService
    {
        private readonly AppSettings _appSettings;
        private readonly DefaultStudent _defaultStudent;
        private readonly ICalendar _calendar;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HostService> _logger;
        

        public HostService(
            IOptions<AppSettings> appSettingsOptions,
            IOptions<DefaultStudent> defaultStudentOptions,
            ICalendar calendar,
            ILogger<HostService> logger,
            IConfiguration configuration
            )
        {
            _appSettings = appSettingsOptions.Value;
            _defaultStudent = defaultStudentOptions.Value;
            _calendar = calendar;
            _logger = logger;
            _configuration = configuration;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start ExecuteAsync");
            int.TryParse(_configuration.GetSection("DelayInMinutes").Value, out var delayInMinutes);
            int.TryParse(_configuration.GetSection("IterationCount").Value, out var iterationCount);

            for (var i = 0; i < iterationCount; i++)
            {
                Console.WriteLine($"CommonConfig FirstName = {_appSettings.FirstName}, LastName = {_appSettings.LastName}");
                Console.WriteLine($"EnvironmentConfig FirstName = {_defaultStudent.FirstName}, LastName = {_defaultStudent.LastName}");
                Console.WriteLine(_calendar.GetCurrentDate());

                await Task.Delay(delayInMinutes, stoppingToken);
            }

            _logger.LogWarning("Stop ExecuteAsync");
        }
    }
}
