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
    public class BoxType : ObjectGraphType<BoxModel>
    {
       public BoxType(IDataLoaderContextAccessor accessor, BoxServiceAdapter boxServiceAdapter, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);

            Field<ShapeType, ShapeModel>()
                .Name("shape")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<long, ShapeModel>(DataLoaderKey.GetShapes, shapeServiceAdapter.GetSingleShapesById);
                    return loader.LoadAsync(ctx.Source.ShapeId);
                });

            Field<ListGraphType<BoxVersionType>, IEnumerable<BoxVersionModel>>()
               .Name("versions")
               .ResolveAsync(ctx =>
               {
                   var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, BoxVersionModel>(DataLoaderKey.GetBoxVersions, boxServiceAdapter.GetVersionsByBoxId);
                   return loader.LoadAsync(ctx.Source.Id);
               });

            Field<BoxVersionType, BoxVersionModel>()
                .Name("actualVersion")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<long, BoxVersionModel>(DataLoaderKey.GetActualBoxVersions, boxServiceAdapter.GetActualVersionsByBoxId);
                    return loader.LoadAsync(ctx.Source.Id);
                });
        }
    }
}
