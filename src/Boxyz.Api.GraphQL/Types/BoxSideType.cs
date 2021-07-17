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
    public class BoxSideType : ObjectGraphType<BoxSideModel>
    {
       public BoxSideType(IHttpContextAccessor httpContextAccessor)
       {
            Field(x => x.Id);

            FieldAsync<ShapeSideType>("shapeSide",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetOne(context.Source.ShapeSideId);
                });

            FieldAsync<ListGraphType<BoxSideCultureType>>("cultures",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IBoxDalService>().GetSideCultures(context.Source.Id);
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
