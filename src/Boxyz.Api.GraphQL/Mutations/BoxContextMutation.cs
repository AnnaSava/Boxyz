using Boxyz.Api.GraphQL.ForDbContext;
using Boxyz.Api.GraphQL.Types;
using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL
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
