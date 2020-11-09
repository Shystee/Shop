using AutoMapper;
using Shop.Contracts.V1.Responses;
using Shop.DataAccess.Entities;

namespace Shop.Api.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<Rating, RatingResponse>();
        }
    }
}