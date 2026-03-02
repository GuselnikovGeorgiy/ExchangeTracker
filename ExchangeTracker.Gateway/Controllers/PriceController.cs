using AutoMapper;
using Core.Abstractions.Models;
using Core.Abstractions.Operations;
using ExchangeTracker.Gateway.Extensions;
using ExchangeTracker.Gateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeTracker.Gateway.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PriceController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<int>> GetPrice(
        [FromQuery] PriceDto priceDto,
        [FromServices] IMapper mapper,
        [FromServices] IExchangePriceQueryOperation queryOperations,
        CancellationToken ct)
    {
        var operationModel = mapper.Map<GetPriceQueryOperationModel>(priceDto);
        var result = await queryOperations.GetPrice(operationModel, ct);
        if (result.IsFailure)
            return result.Error.ToResponse();
        return result.Value;
    }
}
