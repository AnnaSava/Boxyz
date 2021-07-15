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
    public class ShapeVersionType : ObjectGraphType<ShapeVersionModel>
    {
       public ShapeVersionType(IHttpContextAccessor httpContextAccessor)
       {
            Field(x => x.Id);
            Field(x => x.IsApproved);            
            Field(x => x.Created);

            FieldAsync<ListGraphType<ShapeSideType>>("sides",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetSides(context.Source.Id);
                });

            FieldAsync<ListGraphType<ShapeVersionCultureType>>("cultures",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetVersionCultures(context.Source.Id);
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
