﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Infrastructure
{
    public class GraphQLSettings
    {
        public PathString GraphQLPath { get; set; }
        public Func<HttpContext, IDictionary<string, object>> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; }
        public bool ExposeExceptions { get; set; }
    }
}
