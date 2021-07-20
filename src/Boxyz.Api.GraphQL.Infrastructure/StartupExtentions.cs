using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boxyz.Api.GraphQL.Infrastructure
{
    public static class StartupExtentions
    {
        //https://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class
        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static void AddGraphQLTypes(this IServiceCollection services, IEnumerable<Type> types)
        {
            types.Where(m => !m.IsAbstract && !m.IsInterface)
               .Where(m => m.IsSubclassOf(typeof(ObjectGraphType))
                    || IsSubclassOfRawGeneric(typeof(ObjectGraphType<>), m)
                    || m.IsSubclassOf(typeof(InputObjectGraphType)))
               .ToList()
               .ForEach(t => services.AddScoped(t));
        }
    }
}
