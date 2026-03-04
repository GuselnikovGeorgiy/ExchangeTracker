using Exchanges.Abstractions;
using Exchanges.Abstractions.Options;
using Microsoft.Extensions.Options;

namespace BinanceExchange;

internal sealed class BinanceExchangeClient(
    IOptions<ExchangeClientOptions> options
    ) : IExchangeClient
{
    public Task<int> GetExchangePairPriceAsync(string pairName, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}