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
    public class BoxSideType : ObjectGraphType<BoxSideModel>
    {
       public BoxSideType(IHttpContextAccessor httpContextAccessor, IDataLoaderContextAccessor accessor, BoxServiceAdapter boxServiceAdapter, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);

            Field<ShapeSideType, ShapeSideModel>()
                .Name("shapeSide")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<long, ShapeSideModel>("GetSingleSidesByVersionId", shapeServiceAdapter.GetSingleSidesByVersionId);
                    return loader.LoadAsync(ctx.Source.ShapeSideId);
                });

            Field<ListGraphType<BoxSideCultureType>, IEnumerable<BoxSideCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, BoxSideCultureModel>("GetBoxSideCulturesBySideId", boxServiceAdapter.GetSideCulturesBySideId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            FieldAsync<BoxSideCultureType>("culture",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IBoxDalService>().GetSideCulture(context.Source.Id, context.GetArgument<string>("culture"));
                });    
        }
    }
}
