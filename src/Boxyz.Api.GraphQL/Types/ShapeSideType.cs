using Boxyz.Proto.Api.GraphQL.Adapters;
using Boxyz.Proto.Api.GraphQL.Schemas;
using Boxyz.Proto.Data;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using System.Collections.Generic;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeSideType : ObjectGraphType<ShapeSideModel>
    {
       public ShapeSideType(IDataLoaderContextAccessor accessor, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);

            Field<ListGraphType<ShapeSideCultureType>, IEnumerable<ShapeSideCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeSideCultureModel>(DataLoaderKey.GetShapeSideCultures, shapeServiceAdapter.GetSideCulturesBySideId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<ShapeSideCultureType, ShapeSideCultureModel>()
               .Name("culture")
               .Argument<StringGraphType>("culture")
               .ResolveAsync(ctx =>
               {
                   var loader = accessor.Context.GetOrAddBatchLoader<(long, string), ShapeSideCultureModel>(DataLoaderKey.GetShapeSideCulture, shapeServiceAdapter.GetSingleSideCultures);
                   return loader.LoadAsync((ctx.Source.Id, ctx.GetArgument<string>("culture")));
               });
        }
    }
}
