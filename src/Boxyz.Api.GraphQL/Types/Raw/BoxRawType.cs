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
    public class BoxRawType : ObjectGraphType<Box>
    {
       public BoxRawType()
       {
            Field(x => x.Id);
            Field(x => x.Shape, type: typeof(ShapeRawType));
            Field(x => x.Versions, nullable: true, type: typeof(ListGraphType<BoxVersionRawType>));
        }
    }
}
