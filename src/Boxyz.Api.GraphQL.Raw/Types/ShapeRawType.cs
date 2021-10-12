using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class ShapeRawType : ObjectGraphType<Shape>
    {
        public ShapeRawType()
        {
            Field(x => x.Id);
            Field(x => x.ConstName);
            Field(x => x.LastUpdated);
            Field(x => x.Versions, nullable: true, type: typeof(ListGraphType<ShapeVersionRawType>));
        }
    }
}
