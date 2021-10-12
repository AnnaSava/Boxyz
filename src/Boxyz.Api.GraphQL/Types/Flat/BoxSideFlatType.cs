using Boxyz.Proto.Data;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class BoxSideFlatType : ObjectGraphType<BoxSideFlatModel>
    {
       public BoxSideFlatType()
       {
            Field(x => x.Id);
            Field(x => x.Value);
            Field(x => x.UniversalValue);
            Field(x => x.Culture);
            Field(x => x.DataType);
            Field(x => x.ConstName);
            Field(x => x.Title);
            //Field(x => x.ShapeSide, type: typeof(ShapeSideFlatType));
        }
    }
}
