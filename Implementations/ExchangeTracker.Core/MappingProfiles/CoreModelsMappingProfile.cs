using AutoMapper;
using Core.Abstractions.Models;
using Exchanges.Abstractions.Models;

namespace ExchangeTracker.Core.MappingProfiles;

internal sealed class CoreModelsMappingProfile : Profile
{
    public CoreModelsMappingProfile()
    {
        CreateMap<GetPairPriceQueryOperationModel, GetPriceExchangeModel>()
            .ForMember(dest => dest, 
                opt => opt.MapFrom(src => src));

        CreateMap<PriceExchangeModel, PairPriceQueryOperationModel>()
            .ForMember(dest => dest, 
                opt => opt.MapFrom(src => src));
    }
}