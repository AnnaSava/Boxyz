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
    public class BoxType : ObjectGraphType<BoxModel>
    {
       public BoxType(IHttpContextAccessor httpContextAccessor)
       {
            Field(x => x.Id);
            FieldAsync<ShapeType>("shape", resolve: async context => 
            {
                using var scope = httpContextAccessor.CreateScope();
                return await scope.GetService<IShapeDalService>().GetOne(context.Source.ShapeId);
            });

            FieldAsync<ListGraphType<BoxVersionType>>("versions",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IBoxDalService>().GetVersions(context.Source.Id);
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
