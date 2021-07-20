using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types.Raw
{
    public class ShapeBoardCultureRawType : ObjectGraphType<ShapeBoardCulture>
    {
        public ShapeBoardCultureRawType()
        {
            Field(x => x.Culture);
            Field(x => x.ContentId);
            Field(x => x.Title);
        }
    }
}
