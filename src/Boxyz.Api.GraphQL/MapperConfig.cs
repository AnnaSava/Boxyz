using AutoMapper;
using Boxyz.Proto.Data.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Boxyz.Proto.Api.GraphQL
{
    public static class MapperConfig
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BoxMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
