using Boxyz.Data.Contract;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxShapeVersionCultureType : ObjectGraphType<BoxShapeVersionCultureModel>
    {
        public BoxShapeVersionCultureType()
        {
            Field(x => x.Culture);
            Field(x => x.ShapeVersionId);
            Field(x => x.Title);
        }
    }
}
