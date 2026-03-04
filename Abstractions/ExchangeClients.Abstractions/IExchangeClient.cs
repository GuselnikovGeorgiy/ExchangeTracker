namespace Exchanges.Abstractions;

public interface IExchangeClient
{
    Task<int> GetExchangePairPriceAsync(string pairName, CancellationToken ct);
}