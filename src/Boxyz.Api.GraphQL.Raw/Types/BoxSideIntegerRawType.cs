using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class BoxSideIntegerRawType : ObjectGraphType<BoxSideInteger>
    {
        public BoxSideIntegerRawType()
        {
            Field(x => x.Value);
            Field(x => x.Measure);
        }
    }
}
