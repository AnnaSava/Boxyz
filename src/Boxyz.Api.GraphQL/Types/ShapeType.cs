using Boxyz.Proto.Api.GraphQL.Adapters;
using Boxyz.Proto.Api.GraphQL.Schemas;
using Boxyz.Proto.Data;
using GraphQL.DataLoader;
using GraphQL.Types;
using System.Collections.Generic;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeType : ObjectGraphType<ShapeModel>
    {
        public ShapeType(IDataLoaderContextAccessor accessor, ShapeServiceAdapter shapeServiceAdapter)
        {
            Field(x => x.Id);
            Field(x => x.ConstName);
            Field(x => x.LastUpdated);

            Field<ListGraphType<ShapeVersionType>, IEnumerable<ShapeVersionModel>>()
                .Name("versions")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeVersionModel>(DataLoaderKey.GetShapeVersions, shapeServiceAdapter.GetVersionsByShapeId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<ShapeVersionType, ShapeVersionModel>()
                .Name("actualVersion")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<long, ShapeVersionModel>(DataLoaderKey.GetActualShapeVersions, shapeServiceAdapter.GetActualVersionsByShapeId);
                    return loader.LoadAsync(ctx.Source.Id);
                });
        }
    }
}
