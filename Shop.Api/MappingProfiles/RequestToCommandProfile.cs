using AutoMapper;
using Shop.Api.Features.Commands;
using Shop.Contracts.V1.Requests;

namespace Shop.Api.MappingProfiles
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            // Identity mappings
            CreateMap<UserLoginRequest, LoginUserCommand>();
            CreateMap<UserRegistrationRequest, RegistrationUserCommand>();
            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();

            // Product mappings
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductCommand>();

            // Rating mappings
            CreateMap<CreateRatingRequest, CreateRatingCommand>();
            CreateMap<UpdateRatingRequest, UpdateRatingCommand>();
        }
    }
}