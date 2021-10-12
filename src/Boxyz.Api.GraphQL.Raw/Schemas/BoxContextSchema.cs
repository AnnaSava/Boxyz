using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Boxyz.Proto.Api.GraphQL.Raw
{
    public class BoxContextSchema : Schema
    {
        public BoxContextSchema(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<BoxContextQuery>();
            //Mutation = serviceProvider.GetRequiredService<BoxContextMutation>();
        }
    }
}
