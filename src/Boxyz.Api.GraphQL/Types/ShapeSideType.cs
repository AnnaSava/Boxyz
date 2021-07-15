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
    public class ShapeSideType : ObjectGraphType<ShapeSideModel>
    {
       public ShapeSideType(IHttpContextAccessor httpContextAccessor)
       {
            Field(x => x.Id);
            Field(x => x.ConstName);            
            Field(x => x.DataType);

            FieldAsync<ListGraphType<ShapeSideCultureType>>("cultures",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeDalService>().GetSideCultures(context.Source.Id);
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
