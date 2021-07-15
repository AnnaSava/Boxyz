using Boxyz.Api.GraphQL.ForDbContext;
using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeType : ObjectGraphType<ShapeModel>
    {
        public ShapeType(IHttpContextAccessor httpContextAccessor)
        {
            Field(x => x.Id);
            Field(x => x.ConstName);
            Field(x => x.LastUpdated);

            FieldAsync<ListGraphType<ShapeVersionType>>("versions",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetVersions(context.Source.Id);
                });

            FieldAsync<ShapeVersionType>("actualVersion",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetActualVersion(context.Source.Id);
                });
        }
    }
}
