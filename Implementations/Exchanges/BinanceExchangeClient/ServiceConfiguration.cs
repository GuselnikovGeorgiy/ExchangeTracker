using Exchanges.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceExchange;

public static class ServiceConfiguration
{
    public static void ConfigureCoreBinanceServices(this IServiceCollection services, string key)
    {
        services.AddKeyedScoped<IExchangeClient, BinanceExchangeClient>(key);
    }
}