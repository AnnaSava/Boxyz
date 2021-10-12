using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class BoxSideCultureRawType : ObjectGraphType<BoxSideCulture>
    {
       public BoxSideCultureRawType()
       {
            Field(x => x.ContentId);
            Field(x => x.Culture);
            Field(x => x.Value);
        }
    }
}
