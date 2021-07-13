using Boxyz.Api.GraphQL.Types;
using Boxyz.Data;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Instrumentation;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // add execution components
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IErrorInfoProvider>(services =>
            {
                var settings = services.GetRequiredService<IOptions<GraphQLSettings>>();
                return new ErrorInfoProvider(new ErrorInfoProviderOptions { ExposeExceptionStackTrace = settings.Value.ExposeExceptions });
            });

            services.AddMapper();
            services.AddBox(Configuration);

            // add graph types
            services.AddScoped<BoxContextQuery>();            
            services.AddScoped<BoxShapeBoardType>();            
            services.AddScoped<BoxShapeBoardCultureType>();
            services.AddScoped<BoxShapeVersionType>();
            services.AddScoped<BoxShapeVersionCultureType>();
            services.AddScoped<BoxShapeSideType>();
            services.AddScoped<BoxShapeSideCultureType>();

            services.AddScoped<BoxContextMutation>();
            services.AddScoped<BoxShapeBoardInputType>();

            // add schema
            services.AddScoped<ISchema, BoxContextSchema>(services =>
            {
                var settings = services.GetRequiredService<IOptions<GraphQLSettings>>();
                var schema = new BoxContextSchema(services);
                if (settings.Value.EnableMetrics)
                {
                    var middlewares = services.GetRequiredService<IEnumerable<IFieldMiddleware>>();
                    foreach (var middleware in middlewares)
                        schema.FieldMiddleware.Use(middleware);
                }
                return schema;
            });

            // add infrastructure stuff
            services.AddHttpContextAccessor();
            services.AddLogging(builder => builder.AddConsole());

            // add options configuration
            services.Configure<GraphQLSettings>(Configuration);

            // add Field Middlewares
            services.AddScoped<IFieldMiddleware, CountFieldMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<GraphQLMiddleware>();
            app.UseGraphQLGraphiQL();
        }
    }
}
