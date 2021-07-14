using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeBoardFlatType : ObjectGraphType<ShapeBoardFlatModel>
    {
       public ShapeBoardFlatType()
       {
            Field(x => x.Id);
            Field(x => x.Name);            
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);
            Field(x => x.Title);
            Field(x => x.Culture);
        }
    }
}
