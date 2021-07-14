using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeSideFlatType : ObjectGraphType<ShapeSideFlatModel>
    {
       public ShapeSideFlatType()
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);
            Field(x => x.Title);
            Field(x => x.Culture);
       }
    }
}
