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
                    var obj = await scope.GetService<IBoxViewService>().GetBoxObject(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
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
                    return await scope.GetService<IShapeBoardService>()
                        .GetOne(context.GetArgument<long>("id"));
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
                    return await scope.GetService<IShapeService>().GetOne(context.GetArgument<long>("id"));
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
                    return await scope.GetService<IBoxService>().GetOne(context.GetArgument<long>("id"));
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
                    return await scope.GetService<IShapeBoardService>().GetAll(context.GetArgument<int>("page"), context.GetArgument<int>("count"));
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
                    return await scope.GetService<IShapeBoardService>().GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
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
                    return await scope.GetService<IShapeService>().GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
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
                    return await scope.GetService<IBoxService>().GetFlat(context.GetArgument<long>("id"), context.GetArgument<string>("culture"));
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
                    return await scope.GetService<IShapeBoardService>().GetAllFlat(context.GetArgument<int>("page"),
                        context.GetArgument<int>("count"), context.GetArgument<string>("culture"));
                }
            );
        }
    }
}
