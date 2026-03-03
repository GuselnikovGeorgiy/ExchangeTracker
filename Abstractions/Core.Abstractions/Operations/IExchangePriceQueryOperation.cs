using Core.Abstractions.Models;

namespace Core.Abstractions.Operations;

public interface IExchangePriceQueryOperation
{
    Task<Result<int>> GetPairPriceAsync(GetPriceQueryOperationModel getPriceOperationModel, CancellationToken ct);
}