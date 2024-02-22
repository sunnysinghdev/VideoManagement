using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Infrastructure.Logging
{
    public static class SerilogExtension
    {
        public static void ConfigureSerilog(this IHostBuilder host, string fileName) 
        {
            host.UseSerilog((context, services, configuration) => {
                configuration
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day);
            });
        }
    }
}
