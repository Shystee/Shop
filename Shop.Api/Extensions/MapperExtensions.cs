using AutoMapper;

namespace Shop.Api.Extensions
{
    public static class MapperExtensions
    {
        public static TDestination MapWithId<TDestination>(this IMapper mapper, object source, object id)
        {
            var result = mapper.Map<TDestination>(source, options => options.Items["Id"] = id);

            return result;
        }
    }
}