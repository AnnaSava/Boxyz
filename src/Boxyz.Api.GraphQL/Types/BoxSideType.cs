using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxSideType : ObjectGraphType<BoxSideModel>
    {
       public BoxSideType()
       {
            Field(x => x.Id);
            Field(x => x.ShapeSide, type: typeof(ShapeSideType));
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<BoxSideCultureType>));

            Field<BoxSideCultureType>("getCulture",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: context => context.Source.Cultures
                    .FirstOrDefault(m => m.BoxSideId == context.Source.Id && m.Culture == context.GetArgument<string>("culture")));
        }
    }
}
