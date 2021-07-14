using Boxyz.Api.GraphQL.ForDbContext;
using Boxyz.Data;
using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class BoxFlatType : ObjectGraphType<BoxFlatModel>
    {
        //public BoxFlatType(IBoxServiceContext srvContext)
        public BoxFlatType(IHttpContextAccessor _httpContextAccessor)
        {
            Field(x => x.Id);
            Field(x => x.Created);
            Field(x => x.IsApproved);
            Field(x => x.VersionId);
            Field(x => x.Culture);

            FieldAsync<ShapeFlatType>("shape", resolve: async context =>
            {
                using var scope = _httpContextAccessor.CreateScope();
                return await scope.GetService<IBoxServiceContext>().ShapeService.GetFlat(context.Source.ShapeId, context.Source.Culture);
            });

            FieldAsync<ListGraphType<BoxSideFlatType>>("sides", resolve: async context =>
            {
                using var scope = _httpContextAccessor.CreateScope();
                return await scope.GetService<IBoxServiceContext>().GetFlatBoxSides(context.Source.VersionId, context.Source.ShapeVersionId, context.Source.Culture);
            });

            //Field<BoxVersionType>("getVersion",
            //    arguments: new QueryArguments(new QueryArgument<BigIntGraphType> { Name = "id" }),
            //    resolve: context => context.Source.Versions
            //        .FirstOrDefault(m => m.Id == context.GetArgument<long>("id")));
        }
    }
}
