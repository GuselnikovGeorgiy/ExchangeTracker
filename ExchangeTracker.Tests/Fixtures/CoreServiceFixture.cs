using BinanceExchange;
using Exchanges.Abstractions.Enums;
using Exchanges.Abstractions.Options;
using ExchangeTracker.Core;
using ExchangeTracker.Gateway.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Tests.Fixtures;

public sealed class CoreServiceFixture : IDisposable
{
    private IServiceCollection? Services { get; set; }
    public IServiceProvider? Provider { get; private set; }
    
    public CoreServiceFixture()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        Services = new ServiceCollection()
            .AddLogging();
        Services.AddOptions<ExchangeClientOptions>().Bind(configuration.GetSection(nameof(ExchangeClientOptions)));
        
        Services.ConfigureMapper();
        Services.ConfigureCoreServices();
        Services.ConfigureCoreBinanceServices(nameof(ExchangeClientTypeEnum.Binance));
        
        Provider = Services.BuildServiceProvider();
        Provider.ValidateMappingProfiles();
    }

    public void Dispose()
    {
        Services = null;
        Provider = null;
    }
}