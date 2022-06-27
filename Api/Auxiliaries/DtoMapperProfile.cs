using AutoMapper;

namespace Api.Auxiliaries;

public class DtoMapperProfile : Profile
{
    public DtoMapperProfile()
    {
        CreateMap<PickupDto, Rental>()
            .ForMember(a => a.PickupOn, b => b.MapFrom(c => c.Occasion))
            .ForMember(a => a.Mileage, b => b.MapFrom(c => -c.Mileage));
    }
}