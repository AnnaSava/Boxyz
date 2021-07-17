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
    public class BoxVersionType : ObjectGraphType<BoxVersionModel>
    {
       public BoxVersionType(IHttpContextAccessor httpContextAccessor)
       {
            Field(x => x.Id);
            Field(x => x.Created);
            Field(x => x.IsApproved);

            FieldAsync<ShapeVersionType>("shapeVersion", 
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetOne(context.Source.ShapeVersionId);
                });

            FieldAsync<ListGraphType<BoxSideType>>("sides",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IBoxDalService>().GetSides(context.Source.Id);
                });
        }
    }
}
