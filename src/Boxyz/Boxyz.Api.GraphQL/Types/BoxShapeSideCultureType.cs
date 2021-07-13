﻿using Boxyz.Data.Contract;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxShapeSideCultureType : ObjectGraphType<BoxShapeSideCultureModel>
    {
        public BoxShapeSideCultureType()
        {
            Field(x => x.Culture);
            Field(x => x.ShapeSideId);
            Field(x => x.Title);
        }
    }
}
