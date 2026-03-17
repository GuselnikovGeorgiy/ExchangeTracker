using AutoMapper;
using ExchangeTracker.Gateway.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Gateway.Configurations;

internal static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        
    }

    public static void ConfigureGatewayProfiles(this IMapperConfigurationExpression mc)
    {
        mc.AddMaps(typeof(GatewayModelsMappingProfile).Assembly);
    }
}