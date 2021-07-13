using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxShapeBoardType : ObjectGraphType<BoxShapeBoardModel>
    {
       public BoxShapeBoardType()
       {
            Field(x => x.Id);
            Field(x => x.Name);            
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);

            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<BoxShapeBoardCultureType>));

            Field<BoxShapeBoardCultureType>("getCulture",
                arguments: new QueryArguments(
                    new QueryArgument<BigIntGraphType> { Name = "boardId" }, 
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: context => context.Source.Cultures
                    .FirstOrDefault(m => m.BoardId == context.GetArgument<long>("boardId") && m.Culture == context.GetArgument<string>("culture")));
        }
    }
}
