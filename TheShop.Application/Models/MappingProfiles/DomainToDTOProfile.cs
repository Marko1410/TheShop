using AutoMapper;
using TheShop.Application.Models.DTOs;
using TheShop.Domain.Models;

namespace TheShop.Application.Models.MappingProfiles
{
    public class DomainToDTOProfile : Profile
    {
        public DomainToDTOProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.OrderAmount))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));
        }
    }
}
