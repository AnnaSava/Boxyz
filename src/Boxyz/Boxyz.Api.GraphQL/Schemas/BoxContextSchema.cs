using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL
{
    public class BoxContextSchema : Schema
    {
        public BoxContextSchema(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<BoxContextQuery>();
            Mutation = serviceProvider.GetRequiredService<BoxContextMutation>();
        }
    }
}
