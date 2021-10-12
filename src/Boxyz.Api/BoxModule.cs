using AutoMapper;
using Boxyz.Proto.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boxyz.Proto.Data
{
    public static class BoxModule
    {
        public static void AddBox(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BoxDbContext>(options =>
            {
                options.UseLazyLoadingProxies();

                options
                    .UseNpgsql(config.GetConnectionString("BoxConnection"), b => b.MigrationsAssembly("Boxyz.Migrations.PostgreSql"))
                    .UseSnakeCaseNamingConvention();
            }); 

            services.AddScoped<IShapeBoardDalService>(s => new ShapeBoardDalService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IShapeDalService>(s => new ShapeDalService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxDalService>(s => new BoxDalService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxService>(s => new BoxService(
                s.GetService<IBoxDalService>(),
                s.GetService<IShapeDalService>(),
                s.GetService<IMapper>()));
        }
    }
}
