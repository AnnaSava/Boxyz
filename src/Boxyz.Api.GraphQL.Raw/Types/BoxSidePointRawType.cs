using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class BoxSidePointRawType : ObjectGraphType<BoxSidePoint>
    {
        public BoxSidePointRawType()
        {
            Field(x => x.X);
            Field(x => x.Y);
            Field(x => x.Z);
        }
    }
}
