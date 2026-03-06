using Exchanges.Abstractions;
using Exchanges.Abstractions.Options;
using Microsoft.Extensions.Options;

namespace BybitExchange;

internal sealed class BybitExchangeClient(
    IOptions<ExchangeClientOptions> options
    ) : IExchangeClient
{
    public Task<decimal> GetExchangePairPriceAsync(string pairName, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}