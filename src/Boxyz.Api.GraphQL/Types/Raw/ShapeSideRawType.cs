using Boxyz.Api.GraphQL.ForDbContext;
using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types.Raw
{
    public class ShapeSideRawType : ObjectGraphType<ShapeSide>
    {
       public ShapeSideRawType(IHttpContextAccessor httpContextAccessor)
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeSideCultureRawType>));
        }
    }
}
