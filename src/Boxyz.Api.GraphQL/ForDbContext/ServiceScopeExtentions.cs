using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.ForDbContext
{
    // https://github.com/fenomeno83/graphql-dotnet-globalization-demo

    public static class ServiceScopeExtensions
    {
        public static T GetService<T>(this IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}
