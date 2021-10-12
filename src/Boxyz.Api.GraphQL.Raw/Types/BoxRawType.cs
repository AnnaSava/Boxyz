using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class BoxRawType : ObjectGraphType<Box>
    {
       public BoxRawType()
       {
            Field(x => x.Id);
            Field(x => x.Shape, type: typeof(ShapeRawType));
            Field(x => x.Versions, nullable: true, type: typeof(ListGraphType<BoxVersionRawType>));
        }
    }
}
