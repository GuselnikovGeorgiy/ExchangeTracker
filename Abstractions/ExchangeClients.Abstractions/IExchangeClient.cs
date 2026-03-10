using Exchanges.Abstractions.Models;

namespace Exchanges.Abstractions;

public interface IExchangeClient
{
    Task<PriceExchangeModel> GetExchangePairPriceAsync(GetPriceExchangeModel pairName, CancellationToken ct);
}