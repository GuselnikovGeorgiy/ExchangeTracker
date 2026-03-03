using Core.Abstractions;
using Core.Abstractions.Models;
using Core.Abstractions.Operations;

namespace ExchangeTracker.Core.Operations;

public sealed class GetPairPriceOperations : IExchangePriceQueryOperation
{
    public Task<Result<int>> GetPairPriceAsync(GetPriceQueryOperationModel getPriceOperationModel, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}