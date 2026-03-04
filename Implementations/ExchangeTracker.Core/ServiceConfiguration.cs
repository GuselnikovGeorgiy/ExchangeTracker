using Core.Abstractions.Models;
using Core.Abstractions.Operations;
using ExchangeTracker.Core.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Core;

public static class ServiceConfiguration
{
    public static void ConfigureCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IExchangePairPriceQueryOperation, GetPairPairPriceQueryOperation>();
    }
}