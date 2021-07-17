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
        public BoxContextQuery(IBoxServiceContext srvContext, IBoxService boxService, IHttpContextAccessor httpContextAccessor)
        {
#if DEBUG
            AddRawQueries(httpContextAccessor);
#endif

            FieldAsync<ShapeBoardType>(
                "hierBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context => await srvContext.ShapeBoardService.GetOne(context.GetArgument<long>("id"))
            );

            FieldAsync<ShapeType>(
                "hierShape",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context => await srvContext.ShapeService.GetOne(context.GetArgument<long>("id"))
            );

            FieldAsync<BoxType>(
                "hierBox",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context => await srvContext.BoxService.GetOne(context.GetArgument<long>("id"))
            );

            FieldAsync<ShapeBoardFlatType>(
                "flatBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context => await srvContext.ShapeBoardService.GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"))
            );

            FieldAsync<ShapeFlatType>(
                "flatShape",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context => await srvContext.ShapeService.GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"))
            );

            FieldAsync<BoxFlatType>(
                "flatBox",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context => await srvContext.BoxService.GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"))
            );

            FieldAsync<StringGraphType>(
                "boxObject",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context =>
                {
                    var obj = await boxService.GetBoxObject(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
                    var json = JsonSerializer.Serialize(obj);
                    return json;
                }              
            );

            FieldAsync<ListGraphType<ShapeBoardType>>(
                "hierBoards",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "page" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "count" }
                ),
                resolve: async context => await srvContext.ShapeBoardService.GetAll(context.GetArgument<int>("page"), context.GetArgument<int>("count"))
            );

            FieldAsync<ListGraphType<ShapeBoardFlatType>>(
                "flatBoards",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "page" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "count" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "culture" }
                ),
                resolve: async context => await srvContext.ShapeBoardService.GetAllFlat(context.GetArgument<int>("page"), 
                    context.GetArgument<int>("count"), context.GetArgument<string>("culture"))
            );
        }


        /// <summary> 
        /// Very experimental
        /// </summary>
        private void AddRawQueries(IHttpContextAccessor httpContextAccessor)
        {
            // Something is broken here
            // An attempt was made to lazy-load navigation 'Cultures.ShapeBoardProxy' after the associated DbContext was disposed.

            FieldAsync<ShapeBoardRawType>(
                "rawBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    var id = context.GetArgument<long>("id");
                    return await scope.GetService<BoxDbContext>().ShapeBoards.FirstOrDefaultAsync(m => m.Id == id);
                }
            );

            //FieldAsync<ShapeRawType>(
            //    "rawShape",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
            //    ),
            //    resolve: async context => await srvContext.ShapeService.GetOne(context.GetArgument<long>("id"))
            //);

            //FieldAsync<BoxRawType>(
            //    "rawBox",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
            //    ),
            //    resolve: async context => await srvContext.BoxService.GetOne(context.GetArgument<long>("id"))
            //);
        }
    }
}
