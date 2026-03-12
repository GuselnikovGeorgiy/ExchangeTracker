using Exchanges.Abstractions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Gateway.Configurations;

internal static class AddOptionsConfiguration
{
    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ExchangeClientOptions>().Bind(configuration.GetSection(nameof(ExchangeClientOptions)));
    }

}