using Boxyz.Proto.Data;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeBoardCultureType : ObjectGraphType<ShapeBoardCultureModel>
    {
        public ShapeBoardCultureType()
        {
            Field(x => x.Culture);
            Field(x => x.ContentId);
            Field(x => x.Title);
        }
    }
}
