using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeFlatType : ObjectGraphType<ShapeFlatModel>
    {
       public ShapeFlatType(IBoxServiceContext srvContext)
       {
            Field(x => x.Id);
            Field(x => x.ConstName);
            Field(x => x.LastUpdated);
            Field(x => x.IsApproved);
            Field(x => x.Created);
            Field(x => x.Title);
            Field(x => x.Culture);
            Field(x => x.VersionId);

            FieldAsync<ListGraphType<ShapeSideFlatType>>("sides", resolve: async context => await srvContext.ShapeService.GetFlatSides(context.Source.VersionId, context.Source.Culture));
        }
    }
}
