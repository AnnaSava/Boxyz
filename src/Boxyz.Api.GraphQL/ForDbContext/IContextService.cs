using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.ForDbContext
{
    // https://github.com/fenomeno83/graphql-dotnet-globalization-demo

    public interface IContextService
    {
        ValidationContext GetValidationContext<T>(T request);

        IServiceScope CreateScope();
    }
}
