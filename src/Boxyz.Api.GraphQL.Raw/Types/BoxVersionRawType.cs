using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class BoxVersionRawType : ObjectGraphType<BoxVersion>
    {
       public BoxVersionRawType()
       {
            Field(x => x.Id);
            Field(x => x.Created);
            Field(x => x.IsApproved);
            Field(x => x.ShapeVersion, type: typeof(ShapeVersionRawType));
            Field(x => x.Sides, nullable: true, type: typeof(ListGraphType<BoxSideRawType>));
        }
    }
}
