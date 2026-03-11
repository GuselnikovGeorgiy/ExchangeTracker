using ExchangeTracker.Gateway.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Tests.Fixtures;

public sealed class AutoMapperDiFixture : IDisposable
{
    public AutoMapperDiFixture()
    {
        Services = new ServiceCollection()
            .AddLogging();
        
        Services.ConfigureMapper();
        
        Provider = Services.BuildServiceProvider();
    }
    
    public IServiceCollection? Services { get; private set; }
    public IServiceProvider? Provider { get; private set; }
    
    public void Dispose()
    {
        Services = null;
        Provider = null;
    }
}