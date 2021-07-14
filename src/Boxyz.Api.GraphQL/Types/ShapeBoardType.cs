using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeBoardType : ObjectGraphType<ShapeBoardModel>
    {
       public ShapeBoardType(IBoxServiceContext srvContext)
       {
            Field(x => x.Id);
            Field(x => x.Name);            
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);

            //   Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeBoardCultureType>));
            Field<ListGraphType<ShapeBoardCultureType>>("cultures", resolve: context => srvContext.ShapeBoardService.GetCultures(context.Source.Id));
        

            //Field<ShapeBoardCultureType>("getCulture",
            //    arguments: new QueryArguments(
            //        new QueryArgument<StringGraphType> { Name = "culture" }),
            //    resolve: context => context.Source.Cultures
            //        .FirstOrDefault(m => m.BoardId == context.Source.Id && m.Culture == context.GetArgument<string>("culture")));
        }
    }
}
