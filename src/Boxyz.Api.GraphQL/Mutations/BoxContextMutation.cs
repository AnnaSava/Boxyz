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
    public class BoxContextMutation : ObjectGraphType<object>
    {
        public BoxContextMutation(IBoxServiceContext srvContext)
        {
            Name = "Mutation";

            Field<ShapeBoardType>(
                "createBoxShapeBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ShapeBoardInputType>> { Name = "boxShapeBoard" }
                ),
                resolve: context =>
                {
                    var board = context.GetArgument<ShapeBoardModel>("boxShapeBoard");
                    return srvContext.ShapeBoardService.Create(board);
                });
        }
    }
}
