using Boxyz.Api.GraphQL.Types;
using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL
{
    public class BoxContextQuery : ObjectGraphType<object>
    {
        public BoxContextQuery(IBoxServiceContext srvContext)
        {
            FieldAsync<ShapeBoardType>(
                "rawBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context => await srvContext.ShapeBoardService.GetOne(context.GetArgument<long>("id"))
            );

            FieldAsync<ShapeType>(
                "rawShape",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context => await srvContext.ShapeService.GetOne(context.GetArgument<long>("id"))
            );

            FieldAsync<BoxType>(
                "rawBox",
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
        }
    }
}
