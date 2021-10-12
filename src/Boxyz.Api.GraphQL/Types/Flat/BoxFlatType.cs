using Boxyz.Proto.Api.GraphQL.ForDbContext;
using Boxyz.Proto.Data;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class BoxFlatType : ObjectGraphType<BoxFlatModel>
    {
        public BoxFlatType(IHttpContextAccessor httpContextAccessor)
        {
            Field(x => x.Id);
            Field(x => x.Created);
            Field(x => x.IsApproved);
            Field(x => x.VersionId);
            Field(x => x.Culture);

            FieldAsync<ShapeFlatType>("shape", resolve: async context =>
            {
                using var scope = httpContextAccessor.CreateScope();
                return await scope.GetService<IShapeDalService>().GetFlat(context.Source.ShapeId, context.Source.Culture);
            });

            FieldAsync<ListGraphType<BoxSideFlatType>>("sides", resolve: async context =>
            {
                using var scope = httpContextAccessor.CreateScope();
                return await scope.GetService<IBoxService>().GetFlatBoxSides(context.Source.VersionId, context.Source.ShapeVersionId, context.Source.Culture);
            });
        }
    }
}
