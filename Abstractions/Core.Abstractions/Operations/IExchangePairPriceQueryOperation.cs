using Core.Abstractions.Models;

namespace Core.Abstractions.Operations;

public interface IExchangePairPriceQueryOperation
{
    Task<Result<int>> GetPairPriceAsync(GetPairPriceQueryOperationModel getPairPriceOperationModel, CancellationToken ct);
}