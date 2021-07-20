using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Raw.Types
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
