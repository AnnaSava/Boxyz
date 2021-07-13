using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxShapeBoardInputType : InputObjectGraphType
    {
        public BoxShapeBoardInputType()
        {
            Name = "BoxShapeBoardInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
