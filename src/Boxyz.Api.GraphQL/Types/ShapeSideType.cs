using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeSideType : ObjectGraphType<ShapeSideModel>
    {
       public ShapeSideType()
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeSideCultureType>));

            Field<ShapeSideCultureType>("getCulture",
                arguments: new QueryArguments(
                    new QueryArgument<BigIntGraphType> { Name = "boardId" }, 
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: context => context.Source.Cultures
                    .FirstOrDefault(m => m.ShapeSideId == context.GetArgument<long>("shapeVersionId") && m.Culture == context.GetArgument<string>("culture")));
        }
    }
}
