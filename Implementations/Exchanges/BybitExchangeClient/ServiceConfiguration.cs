using Exchanges.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BybitExchange;

public static class ServiceConfiguration
{
    public static void ConfigureCoreBybitServices(this IServiceCollection services, string key)
    {
        services.AddKeyedScoped<IExchangeClient, BybitExchangeClient>(key);
    }
}