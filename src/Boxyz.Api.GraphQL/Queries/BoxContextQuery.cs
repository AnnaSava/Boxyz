using Boxyz.Api.GraphQL.ForDbContext;
using Boxyz.Api.GraphQL.Types;
using Boxyz.Api.GraphQL.Types.Raw;
using Boxyz.Data;
using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL
{
    public class BoxContextQuery : ObjectGraphType<object>
    {
        public BoxContextQuery(IHttpContextAccessor httpContextAccessor, BoxDbContext dbContext)
        {
#if DEBUG
            AddRawEntitiesQueries(httpContextAccessor, dbContext);
#endif
            AddFlatModelQueries(httpContextAccessor);
            AddHierarchicModelQueries(httpContextAccessor);

            FieldAsync<StringGraphType>(
                "boxObject",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    var obj = await scope.GetService<IBoxService>().GetBoxObject(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
                    var json = JsonSerializer.Serialize(obj);
                    return json;
                }
            );
        }

        private void AddHierarchicModelQueries(IHttpContextAccessor httpContextAccessor)
        {
            FieldAsync<ShapeBoardType>(
                "hierBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeBoardDalService>().GetOne(context.GetArgument<long>("id"));
                }
            );

            FieldAsync<ShapeType>(
                "hierShape",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetOne(context.GetArgument<long>("id"));
                }
            );

            FieldAsync<BoxType>(
                "hierBox",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IBoxDalService>().GetOne(context.GetArgument<long>("id"));
                }
            );

            FieldAsync<ListGraphType<ShapeBoardType>>(
                "hierBoards",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "page" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "count" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeBoardDalService>().GetAll(context.GetArgument<int>("page"), context.GetArgument<int>("count"));
                }
            );
        }

        private void AddFlatModelQueries(IHttpContextAccessor httpContextAccessor)
        {
            FieldAsync<ShapeBoardFlatType>(
                "flatBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeBoardDalService>().GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
                }
            );

            FieldAsync<ShapeFlatType>(
                "flatShape",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
                }
            );

            FieldAsync<BoxFlatType>(
                "flatBox",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IBoxDalService>().GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
                }
            );

            FieldAsync<ListGraphType<ShapeBoardFlatType>>(
                "flatBoards",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "page" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "count" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeBoardDalService>().GetAllFlat(context.GetArgument<int>("page"),
                        context.GetArgument<int>("count"), context.GetArgument<string>("culture"));
                }
            );
        }

        /// <summary> 
        /// Very experimental method
        /// Here the classes of DAL are used directly
        /// </summary>
        private void AddRawEntitiesQueries(IHttpContextAccessor httpContextAccessor, BoxDbContext dbContext)
        {
            // Something is broken here
            // Lazy-loading vs. multi-thread dbcontext access

            // Set this false if you want the program to fail on the attempt to use one instance of dbContext in all resolvers
            // instead of failing on using lazy-loading
            bool failOnLazyLoading = true;

            FieldAsync<ShapeBoardRawType>(
               "rawBoard",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
               ),
               resolve: async context =>
               {
                   var id = context.GetArgument<long>("id");
                   if (failOnLazyLoading)
                       return await dbContext.ShapeBoards.FirstOrDefaultAsync(m => m.Id == id);

                   using var scope = httpContextAccessor.CreateScope();
                   return await scope.GetService<BoxDbContext>().ShapeBoards.FirstOrDefaultAsync(m => m.Id == id);
               }
           );

            FieldAsync<ShapeRawType>(
                "rawShape",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<long>("id");
                    if (failOnLazyLoading)
                        return await dbContext.Shapes.FirstOrDefaultAsync(m => m.Id == id);

                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<BoxDbContext>().Shapes.FirstOrDefaultAsync(m => m.Id == id);
                }
            );

            FieldAsync<BoxRawType>(
                "rawBox",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<long>("id");
                    if (failOnLazyLoading)
                        return await dbContext.Boxes.FirstOrDefaultAsync(m => m.Id == id);

                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<BoxDbContext>().Boxes.FirstOrDefaultAsync(m => m.Id == id);
                }
            );
        }
    }
}
