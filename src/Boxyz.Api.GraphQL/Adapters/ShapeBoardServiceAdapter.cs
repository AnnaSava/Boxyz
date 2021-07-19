using Boxyz.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Adapters
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
            return cultures.ToLookup(c => c.BoardId);
        }

        public async Task<IDictionary<(long, string), ShapeBoardCultureModel>> GetSingleCulturesByKey(IEnumerable<(long, string)> keys, CancellationToken cancellationToken)
        {
            var cultures = await _shapeBoardDalService.GetCultures(keys);
            return cultures.ToDictionary(c => (c.BoardId, c.Culture));
        }
    }
}
