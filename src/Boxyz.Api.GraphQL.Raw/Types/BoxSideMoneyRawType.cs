using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Raw.Types
{
    public class BoxSideMoneyRawType : ObjectGraphType<BoxSideMoney>
    {
        public BoxSideMoneyRawType()
        {
            Field(x => x.Value);
            Field(x => x.Currency);
        }
    }
}
