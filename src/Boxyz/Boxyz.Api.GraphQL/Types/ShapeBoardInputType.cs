using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeBoardInputType : InputObjectGraphType
    {
        public ShapeBoardInputType()
        {
            Name = "BoxShapeBoardInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
