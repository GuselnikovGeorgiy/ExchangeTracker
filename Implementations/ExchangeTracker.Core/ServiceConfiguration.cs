using AutoMapper;
using Core.Abstractions.Models;
using Core.Abstractions.Operations;
using ExchangeTracker.Core.MappingProfiles;
using ExchangeTracker.Core.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Core;

public static class ServiceConfiguration
{
    public static void ConfigureCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IGetExchangePairPriceQueryOperation, GetExchangePairPriceQueryOperation>();
    }

    public static void ConfigureCoreProfiles(this IMapperConfigurationExpression mc)
    {
        mc.AddMaps(typeof(CoreModelsMappingProfile).Assembly);
    }
}