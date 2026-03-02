using Core.Abstractions.Models;

namespace Core.Abstractions.Operations;

public interface IExchangePriceQueryOperation
{
    Task<Result<int>> GetPrice(GetPriceQueryOperationModel getPriceOperationModel, CancellationToken ct);
}