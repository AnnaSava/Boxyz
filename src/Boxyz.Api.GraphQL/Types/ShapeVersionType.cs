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
