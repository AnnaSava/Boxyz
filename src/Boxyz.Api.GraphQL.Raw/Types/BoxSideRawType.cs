using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Raw.Types
{
    public class BoxSideRawType : ObjectGraphType<BoxSide>
    {
       public BoxSideRawType()
       {
            Field(x => x.Id);
            Field(x => x.ShapeSide, type: typeof(ShapeSideRawType) );
            Field(x => x.BoxVersion, type: typeof(BoxVersionRawType));
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<BoxSideCultureRawType>));
        }
    }
}
