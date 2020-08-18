using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace NetCoreCheckDemoWebApp
{
    public class BackTask : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var time = new TimeSpan(0, 0, 0, 50);

            await Task.Delay((int)time.TotalMilliseconds, stoppingToken);

            var a = 1;
        }
    }
}
