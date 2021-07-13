using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxShapeType : ObjectGraphType<BoxShapeModel>
    {
       public BoxShapeType()
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.LastUpdated);
            Field(x => x.Versions, nullable: true, type: typeof(ListGraphType<BoxShapeVersionType>));

            Field<BoxShapeVersionType>("getVersion",
                arguments: new QueryArguments(new QueryArgument<BigIntGraphType> { Name = "id" }),
                resolve: context => context.Source.Versions
                    .FirstOrDefault(m => m.Id == context.GetArgument<long>("id")));
        }
    }
}
