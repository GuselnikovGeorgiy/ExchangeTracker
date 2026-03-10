using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using Exchanges.Abstractions;
using Exchanges.Abstractions.Models;
using Exchanges.Abstractions.Options;
using Microsoft.Extensions.Options;

namespace BybitExchange;

internal sealed class BybitExchangeClient(
    IOptions<ExchangeClientOptions> options
    ) : IExchangeClient
{
    public Task<PriceExchangeModel> GetExchangePairPriceAsync(GetPriceExchangeModel pairName, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}