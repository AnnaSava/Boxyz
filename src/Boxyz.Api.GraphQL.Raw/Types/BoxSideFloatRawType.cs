using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class BoxSideFloatRawType : ObjectGraphType<BoxSideFloat>
    {
        public BoxSideFloatRawType()
        {
            Field(x => x.Value);
            Field(x => x.Measure);
        }
    }
}
