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
    public class BoxVersionRawType : ObjectGraphType<BoxVersion>
    {
       public BoxVersionRawType()
       {
            Field(x => x.Id);
            Field(x => x.Created);
            Field(x => x.IsApproved);
            Field(x => x.ShapeVersion, type: typeof(ShapeVersionRawType));
            Field(x => x.Sides, nullable: true, type: typeof(ListGraphType<BoxSideRawType>));
        }
    }
}
