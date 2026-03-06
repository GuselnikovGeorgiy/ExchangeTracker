using Core.Abstractions.Models;

namespace Core.Abstractions.Operations;

public interface IGetExchangePairPriceQueryOperation
{
    Task<Result<PairPriceQueryOperationModel>> GetExchangePairPriceAsync(
        GetPairPriceQueryOperationModel getPairPriceOperationModel,
        CancellationToken ct);
}