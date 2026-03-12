using System.Net.Http.Json;
using AutoMapper;
using Exchanges.Abstractions;
using Exchanges.Abstractions.Models;
using Exchanges.Abstractions.Options;
using Microsoft.Extensions.Options;

namespace BinanceExchange;

internal sealed class BinanceExchangeClient(
    IOptions<ExchangeClientOptions> options,
    IMapper mapper,
    IHttpClientFactory httpClientFactory
    ) : IExchangeClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    
    public async Task<PriceExchangeModel> GetExchangePairPriceAsync(
        GetPriceExchangeModel pairName,
        CancellationToken ct)
    {
        var url = new Uri($"{options.Value.BinanceUrl}ticker/price?symbol={pairName.PairName}");

        var response = await _httpClient.GetAsync(url, ct);

        var responseModel = await response.Content.ReadFromJsonAsync<PriceExchangeResponseModel>(ct);

        return mapper.Map<PriceExchangeModel>(responseModel) 
               ?? throw new InvalidOperationException();
    }
}