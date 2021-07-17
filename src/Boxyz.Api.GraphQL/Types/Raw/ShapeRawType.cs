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
    public class ShapeRawType : ObjectGraphType<Shape>
    {
        public ShapeRawType()
        {
            Field(x => x.Id);
            Field(x => x.ConstName);
            Field(x => x.LastUpdated);
            Field(x => x.Versions, nullable: true, type: typeof(ListGraphType<ShapeVersionRawType>));
        }
    }
}
