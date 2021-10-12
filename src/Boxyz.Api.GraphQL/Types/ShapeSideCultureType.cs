using Boxyz.Proto.Data;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeSideCultureType : ObjectGraphType<ShapeSideCultureModel>
    {
        public ShapeSideCultureType()
        {
            Field(x => x.Culture);
            Field(x => x.ContentId);
            Field(x => x.Title);
        }
    }
}
