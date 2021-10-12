using Boxyz.Proto.Data.Entities;
using GraphQL.Types;

namespace Boxyz.Proto.Api.GraphQL.Raw.Types
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
