using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
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
