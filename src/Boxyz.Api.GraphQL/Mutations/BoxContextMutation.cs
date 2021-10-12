using Boxyz.Proto.Api.GraphQL.Types;
using Boxyz.Proto.Api.GraphQL.ForDbContext;
using Boxyz.Proto.Data;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace Boxyz.Proto.Api.GraphQL
{
    public class BoxContextMutation : ObjectGraphType<object>
    {
        public BoxContextMutation(IHttpContextAccessor httpContextAccessor)
        {
            FieldAsync<ShapeBoardType>(
                "createShapeBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ShapeBoardInputType>> { Name = "shapeBoard" }
                ),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    var board = context.GetArgument<ShapeBoardInputModel>("shapeBoard");
                    return await scope.GetService<IShapeBoardDalService>().Create(board);
                });
        }
    }
}
