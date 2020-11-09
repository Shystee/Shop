using AutoMapper;
using Shop.Api.Domain;
using Shop.Contracts.V1.Requests.Queries;

namespace Shop.Api.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<SortQuery, SortingFilter>();
            CreateMap<GetAllProductsQuery, GetAllProductsFilter>();
            CreateMap<GetAllRatingsQuery, GetAllRatingsFilter>();
        }
    }
}