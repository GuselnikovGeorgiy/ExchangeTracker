using AutoMapper;
using ExchangeTracker.Core;
using ExchangeTracker.Gateway.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Gateway.Configurations;

public static class AutoMapperConfiguration
{
    public static void ConfigureMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(mc =>
        {
            mc.ConfigureGatewayProfiles();
            mc.ConfigureCoreProfiles();
        });
    }

    public static void ValidateMappingProfiles(this IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<IMapper>()
            .ConfigurationProvider
            .AssertConfigurationIsValid();
    }
}
