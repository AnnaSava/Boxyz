using Boxyz.Proto.Api.GraphQL.ForDbContext;
using Boxyz.Proto.Data;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace Boxyz.Proto.Api.GraphQL.Types
{
    public class ShapeFlatType : ObjectGraphType<ShapeFlatModel>
    {
        public ShapeFlatType(IHttpContextAccessor httpContextAccessor)
        {
            Field(x => x.Id);
            Field(x => x.ConstName);
            Field(x => x.LastUpdated);
            Field(x => x.IsApproved);
            Field(x => x.Created);
            Field(x => x.Title);
            Field(x => x.Culture);
            Field(x => x.VersionId);

            FieldAsync<ListGraphType<ShapeSideFlatType>>("sides",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeService>().GetFlatSides(context.Source.VersionId, context.Source.Culture);
                });
        }
    }
}
