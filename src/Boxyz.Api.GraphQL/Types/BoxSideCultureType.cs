using Boxyz.Proto.Data;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class BoxSideCultureType : ObjectGraphType<BoxSideCultureModel>
    {
       public BoxSideCultureType()
       {
            Field(x => x.ContentId);
            Field(x => x.Culture);
            Field(x => x.Value);
        }
    }
}
