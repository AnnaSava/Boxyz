using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class ShapeSideRawType : ObjectGraphType<ShapeSide>
    {
       public ShapeSideRawType()
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeSideCultureRawType>));
        }
    }
}
