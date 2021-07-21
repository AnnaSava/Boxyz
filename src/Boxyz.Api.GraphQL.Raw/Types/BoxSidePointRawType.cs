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
    public class BoxSidePointRawType : ObjectGraphType<BoxSidePoint>
    {
        public BoxSidePointRawType()
        {
            Field(x => x.X);
            Field(x => x.Y);
            Field(x => x.Z);
        }
    }
}
