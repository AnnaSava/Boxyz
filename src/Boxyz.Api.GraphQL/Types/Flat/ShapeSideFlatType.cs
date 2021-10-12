using Boxyz.Proto.Data;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeSideFlatType : ObjectGraphType<ShapeSideFlatModel>
    {
       public ShapeSideFlatType()
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);
            Field(x => x.Title);
            Field(x => x.Culture);
       }
    }
}
