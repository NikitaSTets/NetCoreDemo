using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NetStandart.Calculator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging.Debug;

namespace NetCoreCheckDemo
{
    public class HostService : IHostedService
    {
        private readonly AppSettings _appSettings;
        private readonly DefaultStudent _defaultStudent;
        private readonly ICalendar _calendar;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HostService> _logger;
        private readonly IOptionsMonitor<TestSettings> _testSettings;
        private readonly CancellationTokenSource _stoppingCts;

        private Task _executingTask;


        public HostService(
            IOptions<AppSettings> appSettingsOptions,
            IOptions<DefaultStudent> defaultStudentOptions,
            IOptionsMonitor<TestSettings> testSettings,
            ICalendar calendar,
            ILogger<HostService> logger,
            IConfiguration configuration)
        {
            _appSettings = appSettingsOptions.Value;
            _defaultStudent = defaultStudentOptions.Value;
            _testSettings = testSettings;
            _calendar = calendar;
            _logger = logger;
            _configuration = configuration;

            _stoppingCts = new CancellationTokenSource();
            _testSettings.OnChange(Listener);
        }


        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (_logger.BeginScope("Execute Async has started"))
                {
                    var root = (IConfigurationRoot)_configuration;
                    var jsonProvider = root.Providers.ToArray()[1] as JsonConfigurationProvider;
                    jsonProvider.TryGet("SomeValue", out string value);
                    var someVAlue = _configuration.GetValue<int>("SomeValue");
                    var timespan = new TimeSpan(0, 0, 0, 30);
                    await Task.Delay(timespan, stoppingToken);
                    var test2Age = _testSettings.Get("Test2").TestAge;
                    var test1Age = _testSettings.Get("Test1").TestAge;
                    if (test1Age == test2Age)
                    {
                        _logger.LogInformation("test1Age == test2Age");
                    }
                    _logger.LogInformation("Start ExecuteAsync");

                    int.TryParse(_configuration.GetSection("DelayInMinutes").Value, out var delayInMinutes);
                    int.TryParse(_configuration.GetSection("IterationCount").Value, out var iterationCount);
                    var a = _testSettings.Get("Test2").TestAge;
                    for (var i = 0; i < iterationCount; i++)
                    {
                        Console.WriteLine($"CommonConfig FirstName = {_appSettings.FirstName}, LastName = {_appSettings.LastName}");
                        Console.WriteLine($"EnvironmentConfig FirstName = {_defaultStudent.FirstName}, LastName = {_defaultStudent.LastName}");
                        Console.WriteLine(_calendar.GetCurrentDate());

                        await Task.Delay(delayInMinutes, stoppingToken);
                    }
                }
            }
        }


        private void Listener(TestSettings obj)
        {
            Console.WriteLine($"Option Monitor Listener Test Age= {obj.TestAge}");
        }
    }
}