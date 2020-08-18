using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCoreCheckDemoWebApp
{
    public class HostedService2 : IHostedService
    {
        private readonly ILogger _logger;


        public HostedService2(ILogger<HostedService2> logger)
        {
            _logger = logger;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting IHostedService registered in Program.cs");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("HostedService2 registered in Program");
            return Task.CompletedTask;
        }
    }
}
