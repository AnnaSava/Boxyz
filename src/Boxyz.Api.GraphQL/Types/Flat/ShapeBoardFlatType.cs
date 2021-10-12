using Boxyz.Proto.Data;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeBoardFlatType : ObjectGraphType<ShapeBoardFlatModel>
    {
       public ShapeBoardFlatType()
       {
            Field(x => x.Id);
            Field(x => x.Name);            
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);
            Field(x => x.Title);
            Field(x => x.Culture);
        }
    }
}
