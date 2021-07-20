using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxSideCultureType : ObjectGraphType<BoxSideCultureModel>
    {
       public BoxSideCultureType()
       {
            Field(x => x.ContentId);
            Field(x => x.Culture);
            Field(x => x.Value);
        }
    }
}
