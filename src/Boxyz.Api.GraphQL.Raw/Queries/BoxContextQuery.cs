using Boxyz.Api.GraphQL.Raw.Types;
using Boxyz.Data;
using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL
{
    public class BoxContextQuery : ObjectGraphType<object>
    {
        public BoxContextQuery(BoxDbContext dbContext)
        {
            AddRawEntitiesQueries(dbContext);
        }

        private void AddRawEntitiesQueries(BoxDbContext dbContext)
        {
            FieldAsync<ShapeBoardRawType>(
               "rawBoard",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
               ),
               resolve: async context =>
               {
                   var id = context.GetArgument<long>("id");
                   return await dbContext.ShapeBoards.FirstOrDefaultAsync(m => m.Id == id);
               }
           );

            FieldAsync<ShapeRawType>(
                "rawShape",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<long>("id");
                    return await dbContext.Shapes.FirstOrDefaultAsync(m => m.Id == id);
                }
            );

            FieldAsync<BoxRawType>(
                "rawBox",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<BigIntGraphType>> { Name = "id" }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<long>("id");
                    return await dbContext.Boxes.FirstOrDefaultAsync(m => m.Id == id);
                }
            );
        }
    }
}
