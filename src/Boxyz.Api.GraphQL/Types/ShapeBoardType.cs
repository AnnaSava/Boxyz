using Boxyz.Api.GraphQL.Adapters;
using Boxyz.Api.GraphQL.ForDbContext;
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
    public class ShapeBoardType : ObjectGraphType<ShapeBoardModel>
    {
        public ShapeBoardType(IHttpContextAccessor httpContextAccessor, IDataLoaderContextAccessor accessor, ShapeBoardServiceAdapter shapeBoardServiceAdapter)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);

            Field<ListGraphType<ShapeBoardCultureType>, IEnumerable<ShapeBoardCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeBoardCultureModel>("GetCulturesByBoardId", shapeBoardServiceAdapter.GetCulturesByBoardId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<ShapeBoardCultureType, ShapeBoardCultureModel>()
                .Name("culture")
                .Argument<StringGraphType>("culture")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<(long, string), ShapeBoardCultureModel>("GetSingleCulturesByKey", shapeBoardServiceAdapter.GetSingleCulturesByKey);
                    return loader.LoadAsync((ctx.Source.Id, ctx.GetArgument<string>("culture")));
                });
        }
    }
}
