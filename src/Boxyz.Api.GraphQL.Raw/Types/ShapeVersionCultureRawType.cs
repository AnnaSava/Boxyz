using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class ShapeVersionCultureRawType : ObjectGraphType<ShapeVersionCulture>
    {
        public ShapeVersionCultureRawType()
        {
            Field(x => x.Culture);
            Field(x => x.ContentId);
            Field(x => x.Title);
        }
    }
}
