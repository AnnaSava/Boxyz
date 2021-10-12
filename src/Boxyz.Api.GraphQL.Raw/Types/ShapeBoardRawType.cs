using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
{
    public class ShapeBoardRawType : ObjectGraphType<ShapeBoard>
    {
        public ShapeBoardRawType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeBoardCultureRawType>));
        }
    }
}
