using Exchanges.Abstractions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Gateway.Configurations;

internal static class AddOptionsConfiguration
{
    public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var asd = configuration.GetSection(nameof(ExchangeClientOptions));
        services.AddOptions<ExchangeClientOptions>().Bind(asd);
    }

}