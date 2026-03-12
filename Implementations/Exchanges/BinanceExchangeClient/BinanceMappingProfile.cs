using AutoMapper;
using Exchanges.Abstractions.Models;

namespace BinanceExchange;

public class BinanceMappingProfile : Profile
{
    public BinanceMappingProfile()
    {
        CreateMap<PriceExchangeResponseModel, PriceExchangeModel>();
    }
}