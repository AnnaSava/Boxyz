using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types.Raw
{
    public class ShapeVersionCultureRawType : ObjectGraphType<ShapeVersionCulture>
    {
        public ShapeVersionCultureRawType()
        {
            Field(x => x.Culture);
            Field(x => x.ShapeVersionId);
            Field(x => x.Title);
        }
    }
}
