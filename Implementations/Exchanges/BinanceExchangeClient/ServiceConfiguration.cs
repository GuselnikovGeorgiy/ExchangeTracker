using AutoMapper;
using Exchanges.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceExchange;

public static class ServiceConfiguration
{
    public static void ConfigureCoreBinanceServices(this IServiceCollection services, string key)
    {
        services.AddHttpClient();
        services.AddKeyedScoped<IExchangeClient, BinanceExchangeClient>(key);
    }
    
    public static void ConfigureBinanceProfiles(this IMapperConfigurationExpression mc)
    {
        mc.AddMaps(typeof(BinanceMappingProfile).Assembly);
    }
}