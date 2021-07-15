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
    public class ShapeBoardType : ObjectGraphType<ShapeBoardModel>
    {
        public ShapeBoardType(IHttpContextAccessor httpContextAccessor)
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Path, nullable: true);
            Field(x => x.Level);

            FieldAsync<ListGraphType<ShapeBoardCultureType>>("cultures",
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeBoardDalService>().GetCultures(context.Source.Id);
                });

            FieldAsync<ShapeBoardCultureType>("culture",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: async context =>
                {
                    using var scope = httpContextAccessor.CreateScope();
                    return await scope.GetService<IShapeBoardDalService>().GetCulture(context.Source.Id, context.GetArgument<string>("culture"));
                });
        }
    }
}
