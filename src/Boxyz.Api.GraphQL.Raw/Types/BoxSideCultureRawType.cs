using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Raw.Types
{
    public class BoxSideCultureRawType : ObjectGraphType<BoxSideCulture>
    {
       public BoxSideCultureRawType()
       {
            Field(x => x.ContentId);
            Field(x => x.Culture);
            Field(x => x.Value);
        }
    }
}
