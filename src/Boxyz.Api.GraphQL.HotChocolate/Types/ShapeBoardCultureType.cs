using Boxyz.Proto.Data;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.HotChocolate.Types
{
    [GraphQLName("ShapeBoardCulture")]
    public class ShapeBoardCultureType
    {
        public string Culture { get; set; }

        public long ContentId { get; set; }

        public string Title { get; set; }

        public ShapeBoardCultureType(ShapeBoardCultureModel model)
        {
            Culture = model.Culture;
            ContentId = model.ContentId;
            Title = model.Title;
        }
    }
}
