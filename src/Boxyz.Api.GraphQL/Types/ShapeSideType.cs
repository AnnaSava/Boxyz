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
    public class ShapeSideType : ObjectGraphType<ShapeSideModel>
    {
       public ShapeSideType(IHttpContextAccessor httpContextAccessor, IDataLoaderContextAccessor accessor, ShapeServiceAdapter shapeServiceAdapter)
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);

            Field<ListGraphType<ShapeSideCultureType>, IEnumerable<ShapeSideCultureModel>>()
                .Name("cultures")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<long, ShapeSideCultureModel>("GetSideCulturesBySideId", shapeServiceAdapter.GetSideCulturesBySideId);
                    return loader.LoadAsync(ctx.Source.Id);
                });

            FieldAsync<ShapeSideCultureType>("culture",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetSideCulture(context.Source.Id, context.GetArgument<string>("culture"));
                });
        }
    }
}
