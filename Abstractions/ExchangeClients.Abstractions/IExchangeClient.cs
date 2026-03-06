namespace Exchanges.Abstractions;

public interface IExchangeClient
{
    Task<decimal> GetExchangePairPriceAsync(string pairName, CancellationToken ct);
}