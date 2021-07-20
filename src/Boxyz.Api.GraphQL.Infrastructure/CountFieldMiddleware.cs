using GraphQL;
using GraphQL.Instrumentation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Raw
{

    public sealed class CountFieldMiddleware : IFieldMiddleware, IDisposable
    {
        private int _count;

        public CountFieldMiddleware(IHttpContextAccessor accessor)
        {
            // these dependencies are not needed here and are used only for demonstration purposes
            Debug.Assert(accessor != null);
        }

        public Task<object> Resolve(IResolveFieldContext context, FieldMiddlewareDelegate next)
        {
            Interlocked.Increment(ref _count);

            return next(context);
        }

        public void Dispose()
        {
            Console.WriteLine($"{_count} fields were executed");
        }
    }
}
