using AutoMapper;
using Core.Abstractions;
using Core.Abstractions.Models;
using Core.Abstractions.Operations;
using Exchanges.Abstractions;
using Exchanges.Abstractions.Models;
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
        
        var requestModel = mapper.Map<GetPriceExchangeModel>(getPairPriceOperationModel);
        
        var price = await clientExchange.GetExchangePairPriceAsync(requestModel, ct);
        
        var responseModel = mapper.Map<PairPriceQueryOperationModel>(price);
        
        return responseModel;
    }
}