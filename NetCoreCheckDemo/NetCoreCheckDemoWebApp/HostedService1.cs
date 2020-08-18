using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCoreCheckDemoWebApp
{
    public class HostedService1 : IHostedService
    {
        private readonly ILogger _logger;


        public HostedService1(ILogger<HostedService2> logger)
        {
            _logger = logger;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting IHostedService registered in Startup");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("HostedService1 registered in Startup");
            return Task.CompletedTask;
        }
    }
}
