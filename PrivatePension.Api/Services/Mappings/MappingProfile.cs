using AutoMapper;
using Domain.Entities;
using Services.DTOs;

namespace Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Contribution, ContributionDto>().ReverseMap();
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
            CreateMap<Contribution, ContributionDto>().ReverseMap();
        }
    }
}
