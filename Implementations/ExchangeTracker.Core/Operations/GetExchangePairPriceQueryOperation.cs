using AutoMapper;
using Core.Abstractions;
using Core.Abstractions.Models;
using Core.Abstractions.Operations;
using Exchanges.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeTracker.Core.Operations;

internal sealed class GetExchangePairPriceQueryOperation(
    IServiceProvider provider,
    IMapper mapper
    ) : IGetExchangePairPriceQueryOperation
{
    public async Task<Result<PairPriceQueryOperationModel>> GetExchangePairPriceAsync(
        GetPairPriceQueryOperationModel getPairPriceOperationModel,
        CancellationToken ct)
    {
        var clientExchange = provider.GetRequiredKeyedService<IExchangeClient>(getPairPriceOperationModel.ExchangeName);
        var price = await clientExchange.GetExchangePairPriceAsync(getPairPriceOperationModel.PairName, ct);
        var model = mapper.Map<PairPriceQueryOperationModel>(price);
        return model;
    }
}