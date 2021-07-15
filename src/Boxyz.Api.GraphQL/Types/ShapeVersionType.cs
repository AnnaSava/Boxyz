﻿using Boxyz.Data.Contract;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Types
{
    public class ShapeVersionType : ObjectGraphType<ShapeVersionModel>
    {
       public ShapeVersionType()
       {
            Field(x => x.Id);
            Field(x => x.IsApproved);            
            Field(x => x.Created);
            Field(x => x.Cultures, nullable: true, type: typeof(ListGraphType<ShapeVersionCultureType>));
            Field(x => x.Sides, nullable: true, type: typeof(ListGraphType<ShapeVersionCultureType>));

            Field<ShapeVersionCultureType>("getCulture",
                arguments: new QueryArguments(
                    new QueryArgument<BigIntGraphType> { Name = "boardId" }, 
                    new QueryArgument<StringGraphType> { Name = "culture" }),
                resolve: context => context.Source.Cultures
                    .FirstOrDefault(m => m.ShapeVersionId == context.GetArgument<long>("shapeVersionId") && m.Culture == context.GetArgument<string>("culture")));
        }
    }
}