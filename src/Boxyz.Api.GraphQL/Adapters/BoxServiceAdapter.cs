using Boxyz.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Api.GraphQL.Adapters
{
    public class BoxServiceAdapter
    {
        private readonly IBoxDalService _boxDalService;

        public BoxServiceAdapter(IBoxDalService boxDalService)
        {
            _boxDalService = boxDalService;
        }

        public async Task<ILookup<long, BoxVersionModel>> GetVersionsByBoxId(IEnumerable<long> boxIds, CancellationToken cancellationToken)
        {
            var versions = await _boxDalService.GetVersionsByBoxId(boxIds);
            return versions.ToLookup(c => c.BoxId);
        }

        public async Task<ILookup<long, BoxSideModel>> GetSidesByVersionId(IEnumerable<long> versionIds, CancellationToken cancellationToken)
        {
            var sides = await _boxDalService.GetSidesByVersionId(versionIds);
            return sides.ToLookup(c => c.BoxVersionId);
        }

        public async Task<ILookup<long, BoxSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds, CancellationToken cancellationToken)
        {
            var cultures = await _boxDalService.GetSideCulturesBySideId(sideIds);
            return cultures.ToLookup(c => c.BoxSideId);
        }
    }
}
