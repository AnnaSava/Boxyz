using Boxyz.Proto.Api.GraphQL.Adapters;
using Boxyz.Proto.Api.GraphQL.Schemas;
using Boxyz.Proto.Data;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using System.Collections.Generic;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeVersionType : ObjectGraphType<ShapeVersionModel>
    {
       public ShapeVersionType(IDataLoaderContextAccessor accessor, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);
            Field(x => x.IsApproved);            
            Field(x => x.Created);

            Field<ListGraphType<ShapeSideType>, IEnumerable<ShapeSideModel>>()
                .Name("sides")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeSideModel>(DataLoaderKey.GetShapeSides, shapeServiceAdapter.GetSidesByVersionId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<ListGraphType<ShapeVersionCultureType>, IEnumerable<ShapeVersionCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeVersionCultureModel>(DataLoaderKey.GetShapeVersionCultures, shapeServiceAdapter.GetVersionCulturesByVersionId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<ShapeVersionCultureType, ShapeVersionCultureModel>()
               .Name("culture")
               .Argument<StringGraphType>("culture")
               .ResolveAsync(ctx =>
               {
                   var loader = accessor.Context.GetOrAddBatchLoader<(long, string), ShapeVersionCultureModel>(DataLoaderKey.GetShapeVersionCulture, shapeServiceAdapter.GetSingleVersionCultures);
                   return loader.LoadAsync((ctx.Source.Id, ctx.GetArgument<string>("culture")));
               });
        }
    }
}
