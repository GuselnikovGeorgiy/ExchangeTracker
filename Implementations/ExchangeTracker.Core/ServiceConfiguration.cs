using System.Runtime.CompilerServices;
using AutoMapper;
using Core.Abstractions.Operations;
using ExchangeTracker.Core.MappingProfiles;
using ExchangeTracker.Core.Operations;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ExchangeTracker.Tests")]
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