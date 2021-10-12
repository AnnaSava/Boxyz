using Boxyz.Proto.Api.GraphQL.Adapters;
using Boxyz.Proto.Api.GraphQL.Schemas;
using Boxyz.Proto.Data;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using System.Collections.Generic;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeBoardType : ObjectGraphType<ShapeBoardModel>
    {
        public ShapeBoardType(IDataLoaderContextAccessor accessor, ShapeBoardServiceAdapter shapeBoardServiceAdapter)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);

            Field<ListGraphType<ShapeBoardCultureType>, IEnumerable<ShapeBoardCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeBoardCultureModel>(DataLoaderKey.GetShapeBoardCultures, shapeBoardServiceAdapter.GetCulturesByBoardId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<ShapeBoardCultureType, ShapeBoardCultureModel>()
                .Name("culture")
                .Argument<StringGraphType>("culture")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<(long, string), ShapeBoardCultureModel>(DataLoaderKey.GetShapeBoardCulture, shapeBoardServiceAdapter.GetSingleCultures);
                    return loader.LoadAsync((ctx.Source.Id, ctx.GetArgument<string>("culture")));
                });
        }
    }
}
