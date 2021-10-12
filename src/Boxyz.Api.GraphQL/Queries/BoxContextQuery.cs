using Boxyz.Proto.Api.GraphQL.ForDbContext;
using Boxyz.Proto.Api.GraphQL.Types;
using Boxyz.Proto.Data;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Boxyz.Proto.Api.GraphQL
{
    public class BoxContextQuery : ObjectGraphType<object>
    {
        public BoxContextQuery(IHttpContextAccessor httpContextAccessor)
        {
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
    }
}
