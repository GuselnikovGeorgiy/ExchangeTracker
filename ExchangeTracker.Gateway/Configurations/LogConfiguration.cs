using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace ExchangeTracker.Gateway.Configurations;

internal static class LogConfiguration
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.AddConsole()
            .AddFilter(level => level >= LogLevel.Error);
    }
}