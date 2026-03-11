using ExchangeTracker.Tests.Fixtures;

namespace ExchangeTracker.Tests;

public class MapperTests
{
    [Fact]
    public void AutoMapper_ShouldAssertConfigurationIsValid_WhenStartApp()
    {
        _ = new AutoMapperDiFixture();
        Assert.True(true);
    }
}