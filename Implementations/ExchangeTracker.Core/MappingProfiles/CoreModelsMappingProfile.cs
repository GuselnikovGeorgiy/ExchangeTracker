using AutoMapper;
using Core.Abstractions.Models;

namespace ExchangeTracker.Core.MappingProfiles;

internal sealed class CoreModelsMappingProfile : Profile
{
    public CoreModelsMappingProfile()
    {
        CreateMap<decimal, PairPriceQueryOperationModel>()
            .ForMember(d => d.Price, opt => opt.MapFrom(s => s));
    }
}