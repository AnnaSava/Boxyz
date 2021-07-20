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
    public class ShapeBoardRawType : ObjectGraphType<ShapeBoard>
    {
        public ShapeBoardRawType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeBoardCultureRawType>));
        }
    }
}
