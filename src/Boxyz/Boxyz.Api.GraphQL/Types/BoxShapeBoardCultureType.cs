using Boxyz.Data.Contract;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxShapeBoardCultureType : ObjectGraphType<BoxShapeBoardCultureModel>
    {
        public BoxShapeBoardCultureType()
        {
            Field(x => x.Culture);
            Field(x => x.BoardId);
            Field(x => x.Title);
        }
    }
}
