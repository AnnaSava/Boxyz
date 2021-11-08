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
            //services.AddDbContext<BoxContext>(options =>
            //{
            //    options.UseLazyLoadingProxies();

            //    options
            //        .UseNpgsql(config.GetConnectionString("BoxConnection"), b => b.MigrationsAssembly("Boxyz.Migrations.PostgreSql"))
            //        .UseSnakeCaseNamingConvention();
            //});

            services.AddPooledDbContextFactory<BoxContext>(options =>
            {
                options.UseLazyLoadingProxies();

                options
                    .UseNpgsql(config.GetConnectionString("BoxConnection"), b => b.MigrationsAssembly("Boxyz.Migrations.PostgreSql"))
                    .UseSnakeCaseNamingConvention();
            });

            //services.AddScoped<BoxContext>(p =>
            //    p.GetRequiredService<IDbContextFactory<BoxContext>>()
            //    .CreateDbContext());

            services.AddScoped<IShapeBoardService>(s => new ShapeBoardService(
                s.GetRequiredService<IDbContextFactory<BoxContext>>().CreateDbContext(),
                s.GetService<IMapper>()));

            services.AddScoped<IShapeService>(s => new ShapeService(
                s.GetService<BoxContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxService>(s => new BoxService(
                s.GetService<BoxContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxViewService>(s => new BoxViewService(
                s.GetService<IBoxService>(),
                s.GetService<IShapeService>(),
                s.GetService<IMapper>()));
        }
    }
}
