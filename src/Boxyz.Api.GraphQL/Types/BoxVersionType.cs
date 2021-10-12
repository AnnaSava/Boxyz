using Boxyz.Proto.Api.GraphQL.Adapters;
using Boxyz.Proto.Api.GraphQL.Schemas;
using Boxyz.Proto.Data;
using GraphQL.DataLoader;
using GraphQL.Types;
using System.Collections.Generic;

namespace Boxyz.Proto.Api.GraphQL.Types
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
