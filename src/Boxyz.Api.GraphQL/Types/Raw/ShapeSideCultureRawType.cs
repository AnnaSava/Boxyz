using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types.Raw
{
    public class ShapeSideCultureRawType : ObjectGraphType<ShapeSideCulture>
    {
        public ShapeSideCultureRawType()
        {
            Field(x => x.Culture);
            Field(x => x.ShapeSideId);
            Field(x => x.Title);
        }
    }
}
