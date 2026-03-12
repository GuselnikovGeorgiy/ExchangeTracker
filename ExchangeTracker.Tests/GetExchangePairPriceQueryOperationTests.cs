using Core.Abstractions.Models;
using Core.Abstractions.Operations;
using ExchangeTracker.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Tests;

public sealed class GetExchangePairPriceQueryOperationTests : IClassFixture<CoreServiceFixture>
{
    private readonly CoreServiceFixture _fixture;

    public GetExchangePairPriceQueryOperationTests(CoreServiceFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetExchangePairPriceQueryOperation_ShouldReturnSuccessWithPrice_WhenBinanceAndValidPair()
    {
        // Arrange
        var operation = _fixture.Provider!.GetRequiredService<IGetExchangePairPriceQueryOperation>();
        var request = new GetPairPriceQueryOperationModel
        {
            ExchangeName = "Binance",
            PairName = "BTCUSDT"
        };

        // Act
        var result = await operation.GetExchangePairPriceAsync(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Price > 0);
    }

    [Fact]
    public async Task GetExchangePairPriceQueryOperation_ShouldReturnSuccessWithPrice_WhenBinanceAndAnotherValidPair()
    {
        // Arrange
        var operation = _fixture.Provider!.GetRequiredService<IGetExchangePairPriceQueryOperation>();
        var request = new GetPairPriceQueryOperationModel
        {
            ExchangeName = "Binance",
            PairName = "ETHUSDT"
        };

        // Act
        var result = await operation.GetExchangePairPriceAsync(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value.Price > 0);
    }

    [Fact]
    public async Task GetExchangePairPriceQueryOperation_ShouldThrow_WhenExchangeNameIsInvalid()
    {
        // Arrange
        var operation = _fixture.Provider!.GetRequiredService<IGetExchangePairPriceQueryOperation>();
        var request = new GetPairPriceQueryOperationModel
        {
            ExchangeName = "NonExistentExchange",
            PairName = "BTCUSDT"
        };

        // Act
        var act = () => operation.GetExchangePairPriceAsync(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task GetExchangePairPriceQueryOperation_ShouldThrow_WhenTickerNotFound()
    {
        // Arrange
        var operation = _fixture.Provider!.GetRequiredService<IGetExchangePairPriceQueryOperation>();
        var request = new GetPairPriceQueryOperationModel
        {
            ExchangeName = "Binance",
            PairName = "INVALIDTICKER123XYZ"
        };

        // Act
        var act = () => operation.GetExchangePairPriceAsync(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsAnyAsync<Exception>(act);
    }

    [Fact]
    public async Task GetExchangePairPriceQueryOperation_ShouldThrow_WhenPairNameIsEmpty()
    {
        // Arrange
        var operation = _fixture.Provider!.GetRequiredService<IGetExchangePairPriceQueryOperation>();
        var request = new GetPairPriceQueryOperationModel
        {
            ExchangeName = "Binance",
            PairName = ""
        };

        // Act
        var act = () => operation.GetExchangePairPriceAsync(request, CancellationToken.None);

        // Assert
        await Assert.ThrowsAnyAsync<Exception>(act);
    }
}
