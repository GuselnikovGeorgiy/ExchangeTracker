using Core.Abstractions;
using Core.Abstractions.Models;
using Core.Abstractions.Operations;
using Exchanges.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Core.Operations;

public sealed class GetPairPairPriceQueryOperation(
    IServiceProvider provider
    ) : IExchangePairPriceQueryOperation
{
    public async Task<Result<int>> GetPairPriceAsync(
        GetPairPriceQueryOperationModel getPairPriceOperationModel, 
        CancellationToken ct)
    {
        var clientExchange = provider.GetRequiredKeyedService<IExchangeClient>(getPairPriceOperationModel.ExchangeName);
        var result = await clientExchange.GetExchangePairPriceAsync(getPairPriceOperationModel.PairName, ct);
        return result;
    }
}