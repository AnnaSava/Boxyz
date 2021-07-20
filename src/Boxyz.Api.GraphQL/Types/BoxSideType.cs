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
    public class BoxSideType : ObjectGraphType<BoxSideModel>
    {
       public BoxSideType(IDataLoaderContextAccessor accessor, BoxServiceAdapter boxServiceAdapter, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);

            Field<ShapeSideType, ShapeSideModel>()
                .Name("shapeSide")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<long, ShapeSideModel>(DataLoaderKey.GetBoxSide, shapeServiceAdapter.GetSingleSidesByVersionId);
                    return loader.LoadAsync(ctx.Source.ShapeSideId);
                });

            Field<ListGraphType<BoxSideCultureType>, IEnumerable<BoxSideCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, BoxSideCultureModel>(DataLoaderKey.GetBoxSideCultures, boxServiceAdapter.GetSideCulturesBySideId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<BoxSideCultureType, BoxSideCultureModel>()
               .Name("culture")
               .Argument<StringGraphType>("culture")
               .ResolveAsync(ctx =>
               {
                   var loader = accessor.Context.GetOrAddBatchLoader<(long, string), BoxSideCultureModel>(DataLoaderKey.GetBoxSideCulture, boxServiceAdapter.GetSingleSideCultures);
                   return loader.LoadAsync((ctx.Source.Id, ctx.GetArgument<string>("culture")));
               });
        }
    }
}
