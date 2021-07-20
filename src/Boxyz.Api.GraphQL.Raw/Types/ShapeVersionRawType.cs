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
    public class ShapeVersionRawType : ObjectGraphType<ShapeVersion>
    {
       public ShapeVersionRawType()
       {
            Field(x => x.Id);
            Field(x => x.IsApproved);            
            Field(x => x.Created);
            Field(x => x.Sides, nullable: true, type: typeof(ListGraphType<ShapeSideRawType>));
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeVersionCultureRawType>));
        }
    }
}
