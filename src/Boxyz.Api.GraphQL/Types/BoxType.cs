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
    public class BoxType : ObjectGraphType<BoxModel>
    {
       public BoxType(IHttpContextAccessor httpContextAccessor, IDataLoaderContextAccessor accessor, BoxServiceAdapter boxServiceAdapter, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);

            Field<ShapeType, ShapeModel>()
                .Name("shape")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<long, ShapeModel>("GetShapesById", shapeServiceAdapter.GetSingleShapesById);
                    return loader.LoadAsync(ctx.Source.ShapeId);
                });

            Field<ListGraphType<BoxVersionType>, IEnumerable<BoxVersionModel>>()
               .Name("versions")
               .ResolveAsync(ctx =>
               {
                   var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, BoxVersionModel>("GetVersionsByBoxId", boxServiceAdapter.GetVersionsByBoxId);
                   return loader.LoadAsync(ctx.Source.Id);
               });

            FieldAsync<BoxVersionType>("actualVersion",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IBoxDalService>().GetActualVersion(context.Source.Id);
                });
        }
    }
}
