using Boxyz.Proto.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.Adapters
{
    public class ShapeBoardServiceAdapter
    {
        private readonly IShapeBoardDalService _shapeBoardDalService;

        public ShapeBoardServiceAdapter(IShapeBoardDalService shapeBoardDalService)
        {
            _shapeBoardDalService = shapeBoardDalService;
        }

        public async Task<ILookup<long, ShapeBoardCultureModel>> GetCulturesByBoardId(IEnumerable<long> boardIds, CancellationToken cancellationToken)
        {
            var cultures = await _shapeBoardDalService.GetCulturesByBoardId(boardIds);
            return cultures.ToLookup(c => c.ContentId);
        }

        public async Task<IDictionary<(long, string), ShapeBoardCultureModel>> GetSingleCultures(IEnumerable<(long, string)> keys, CancellationToken cancellationToken)
        {
            var cultures = await _shapeBoardDalService.GetCultures(keys);
            return cultures.ToDictionary(c => (c.ContentId, c.Culture));
        }
    }
}
