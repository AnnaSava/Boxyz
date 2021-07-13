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
                    .UseNpgsql(config.GetConnectionString("BoxConnection"), b => b.MigrationsAssembly("Boxys.Migrations.PostgreSql"))
                    .UseSnakeCaseNamingConvention();
            }); 

            services.AddScoped<IBoxShapeBoardService>(s => new BoxShapeBoardService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxShapeService>(s => new BoxShapeService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxService>(s => new BoxService(
                s.GetService<BoxDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IBoxServiceContext>(s => new BoxServiceContext(
                s.GetService<IBoxShapeBoardService>(),
                s.GetService<IBoxShapeService>(),
                s.GetService<IBoxService>()));
        }
    }
}
