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
            Field<ShapeBoardType>(
                "getBoard",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: context => srvContext.ShapeBoardService.GetOne(context.GetArgument<long>("id"))
            );
        }
    }
}
