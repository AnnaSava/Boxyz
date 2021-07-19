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
    public class ShapeVersionType : ObjectGraphType<ShapeVersionModel>
    {
       public ShapeVersionType(IHttpContextAccessor httpContextAccessor, IShapeDalService shapeDalService, IDataLoaderContextAccessor accessor, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);
            Field(x => x.IsApproved);            
            Field(x => x.Created);

            Field<ListGraphType<ShapeSideType>, IEnumerable<ShapeSideModel>>()
                .Name("sides")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeSideModel>("GetSidesByVersionId", shapeServiceAdapter.GetSidesByVersionId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            Field<ListGraphType<ShapeVersionCultureType>, IEnumerable<ShapeVersionCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeVersionCultureModel>("GetVersionCulturesByVersionId", shapeServiceAdapter.GetVersionCulturesByVersionId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            FieldAsync<ShapeVersionCultureType>("culture",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetVersionCulture(context.Source.Id, context.GetArgument<string>("culture"));
                });
        }
    }
}
