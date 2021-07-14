using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxVersionType : ObjectGraphType<BoxVersionModel>
    {
       public BoxVersionType()
       {
            Field(x => x.Id);
            Field(x => x.Created);
            Field(x => x.IsApproved);
            Field(x => x.ShapeVersion, type: typeof(ShapeVersionType));
            Field(x => x.Sides, nullable: true, type: typeof(ListGraphType<BoxSideType>));

            Field<BoxSideType>("getSide",
                arguments: new QueryArguments(new QueryArgument<BigIntGraphType> { Name = "id" }),
                resolve: context => context.Source.Sides
                    .FirstOrDefault(m => m.Id == context.GetArgument<long>("id")));
        }
    }
}
