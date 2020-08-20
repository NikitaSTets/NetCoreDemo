using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCoreCheckDemoWebApp
{
    public class HostedService1 : IHostedService
    {
        private readonly ILogger<HostedService1> _logger;


        public HostedService1(ILogger<HostedService1> logger)
        {
            _logger = logger;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting IHostedService registered in Startup");

            //await Task.Delay(50000);

            //var a = 5;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("HostedService1 registered in Startup");
            return Task.CompletedTask;
        }
    }
}
