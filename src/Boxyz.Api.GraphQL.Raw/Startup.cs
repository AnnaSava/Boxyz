using Boxyz.Proto.Api.GraphQL.Infrastructure;
using Boxyz.Proto.Data.Services;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Instrumentation;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection;

namespace Boxyz.Proto.Api.GraphQL.Raw
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
            services.AddSingleton<IDocumentExecuter, SerialDocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IErrorInfoProvider>(services =>
            {
                var settings = services.GetRequiredService<IOptions<GraphQLSettings>>();
                return new ErrorInfoProvider(new ErrorInfoProviderOptions { ExposeExceptionStackTrace = settings.Value.ExposeExceptions });
            });

            services.AddDbContext<BoxContext>(options =>
            {
                options.UseLazyLoadingProxies();

                options
                    .UseNpgsql(Configuration.GetConnectionString("BoxConnection"), b => b.MigrationsAssembly("Boxyz.Migrations.PostgreSql"))
                    .UseSnakeCaseNamingConvention();
            });

            // add graph types
            services.AddGraphQLTypes(Assembly.GetAssembly(typeof(BoxContextSchema)).GetTypes());

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
