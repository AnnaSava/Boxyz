using Boxyz.Api.GraphQL.Adapters;
using Boxyz.Api.GraphQL.ForDbContext;
using Boxyz.Api.GraphQL.Infrastructure;
using Boxyz.Api.GraphQL.Types;
using Boxyz.Data;
using GraphQL;
using GraphQL.DataLoader;
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
using System.Reflection;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL
{
    public class Startup
    {
        //https://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class
        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // add execution components
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>(); // default DocumentExecutor works in parallel
            //services.AddSingleton<IDocumentExecuter, SerialDocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IErrorInfoProvider>(services =>
            {
                var settings = services.GetRequiredService<IOptions<GraphQLSettings>>();
                return new ErrorInfoProvider(new ErrorInfoProviderOptions { ExposeExceptionStackTrace = settings.Value.ExposeExceptions });
            });

            // https://fiyazhasan.me/graphql-with-net-core-part-x-execution-strategies/
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddMapper();
            services.AddBox(Configuration);
            services.AddScoped<IContextService, ContextService>();

            // adapters for DataLoader
            services.AddScoped<ShapeBoardServiceAdapter>();
            services.AddScoped<ShapeServiceAdapter>();
            services.AddScoped<BoxServiceAdapter>();

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
