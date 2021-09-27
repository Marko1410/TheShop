using AutoMapper;
using TheShop.Application.Models.Reponses;
using TheShop.Domain.Models;

namespace TheShop.Application.Models.MappingProfiles
{
    public class ResponseToDomainModelsProfile : Profile
    {
        public ResponseToDomainModelsProfile()
        {
            CreateMap<ProductAvailabilityResponse, ProductAvailability>();
        }
    }
}
