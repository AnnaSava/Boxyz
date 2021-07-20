using Boxyz.Api.GraphQL.Adapters;
using Boxyz.Api.GraphQL.ForDbContext;
using Boxyz.Api.GraphQL.Schemas;
using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxVersionType : ObjectGraphType<BoxVersionModel>
    {
       public BoxVersionType(IDataLoaderContextAccessor accessor, BoxServiceAdapter boxServiceAdapter, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);
            Field(x => x.Created);
            Field(x => x.IsApproved);

            Field<ShapeVersionType, ShapeVersionModel>()
                .Name("shapeVersion")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<long, ShapeVersionModel>(DataLoaderKey.GetBoxVersion, shapeServiceAdapter.GetSingleVersionsByShapeId);
                    return loader.LoadAsync(ctx.Source.ShapeVersionId);
                });

            Field<ListGraphType<BoxSideType>, IEnumerable<BoxSideModel>>()
               .Name("sides")
               .ResolveAsync(ctx =>
               {
                   var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, BoxSideModel>(DataLoaderKey.GetBoxSides, boxServiceAdapter.GetSidesByVersionId);
                   return loader.LoadAsync(ctx.Source.Id);
               });
        }
    }
}
