using Boxyz.Proto.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.Adapters
{
    public class BoxServiceAdapter
    {
        private readonly IBoxService _boxService;

        public BoxServiceAdapter(IBoxService boxService)
        {
            _boxService = boxService;
        }

        public async Task<ILookup<long, BoxVersionModel>> GetVersionsByBoxId(IEnumerable<long> boxIds, CancellationToken cancellationToken)
        {
            var versions = await _boxService.GetVersionsByBoxId(boxIds);
            return versions.ToLookup(c => c.ContentId);
        }

        public async Task<IDictionary<long, BoxVersionModel>> GetActualVersionsByBoxId(IEnumerable<long> boxIds, CancellationToken cancellationToken)
        {
            var versions = await _boxService.GetActualVersions(boxIds);
            return versions.ToDictionary(c => c.ContentId);
        }

        public async Task<ILookup<long, BoxSideModel>> GetSidesByVersionId(IEnumerable<long> versionIds, CancellationToken cancellationToken)
        {
            var sides = await _boxService.GetSidesByVersionId(versionIds);
            return sides.ToLookup(c => c.BoxVersionId);
        }

        public async Task<ILookup<long, BoxSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds, CancellationToken cancellationToken)
        {
            var cultures = await _boxService.GetSideCulturesBySideId(sideIds);
            return cultures.ToLookup(c => c.ContentId);
        }

        public async Task<IDictionary<(long, string), BoxSideCultureModel>> GetSingleSideCultures(IEnumerable<(long, string)> keys, CancellationToken cancellationToken)
        {
            var cultures = await _boxService.GetSideCultures(keys);
            return cultures.ToDictionary(c => (c.ContentId, c.Culture));
        }
    }
}
