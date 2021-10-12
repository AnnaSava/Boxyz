using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class ShapeVersionRawType : ObjectGraphType<ShapeVersion>
    {
       public ShapeVersionRawType()
       {
            Field(x => x.Id);
            Field(x => x.IsApproved);            
            Field(x => x.Created);
            Field(x => x.Sides, nullable: true, type: typeof(ListGraphType<ShapeSideRawType>));
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeVersionCultureRawType>));
        }
    }
}
