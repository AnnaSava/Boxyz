using Boxyz.Proto.Api.GraphQL.Raw.Types;
using Boxyz.Proto.Data.Services;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Boxyz.Proto.Api.GraphQL
{
    public class BoxContextQuery : ObjectGraphType<object>
    {
        public BoxContextQuery(BoxContext dbContext)
        {
            AddRawEntitiesQueries(dbContext);
        }

        private void AddRawEntitiesQueries(BoxContext dbContext)
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
