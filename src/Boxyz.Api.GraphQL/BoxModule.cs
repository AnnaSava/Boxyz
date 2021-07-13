using AutoMapper;
using Boxyz.Data.Contract;
using Boxyz.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data
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

            services.AddScoped<IShapeBoardService>(s => new ShapeBoardService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IShapeService>(s => new ShapeService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxService>(s => new BoxService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxServiceContext>(s => new BoxServiceContext(
                s.GetService<IShapeBoardService>(),
                s.GetService<IShapeService>(),
                s.GetService<IBoxService>()));
        }
    }
}
