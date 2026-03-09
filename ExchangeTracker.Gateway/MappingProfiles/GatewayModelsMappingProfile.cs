using AutoMapper;
using Core.Abstractions.Models;
using ExchangeTracker.Gateway.Models;

namespace ExchangeTracker.Gateway.MappingProfiles;

internal sealed class GatewayModelsMappingProfile : Profile
{
    public GatewayModelsMappingProfile()
    {
        CreateMap<PriceDto, GetPairPriceQueryOperationModel>();
    }
}