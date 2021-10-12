using Boxyz.Proto.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.Adapters
{
    public class ShapeServiceAdapter
    {
        private readonly IShapeService _shapeService;

        public ShapeServiceAdapter(IShapeService shapeService)
        {
            _shapeService = shapeService;
        }

        public async Task<ILookup<long, ShapeVersionModel>> GetVersionsByShapeId(IEnumerable<long> shapeIds, CancellationToken cancellationToken)
        {
            var versions = await _shapeService.GetVersionsByShapeId(shapeIds);
            return versions.ToLookup(c => c.ContentId);
        }

        public async Task<ILookup<long, ShapeVersionCultureModel>> GetVersionCulturesByVersionId(IEnumerable<long> versionIds, CancellationToken cancellationToken)
        {
            var cultures = await _shapeService.GetVersionCulturesByVersionId(versionIds);
            return cultures.ToLookup(c => c.ContentId);
        }

        public async Task<ILookup<long, ShapeSideModel>> GetSidesByVersionId(IEnumerable<long> versionIds, CancellationToken cancellationToken)
        {
            var sides = await _shapeService.GetSidesByVersionId(versionIds);
            return sides.ToLookup(c => c.ShapeVersionId);
        }

        public async Task<ILookup<long, ShapeSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds, CancellationToken cancellationToken)
        {
            var cultures = await _shapeService.GetSideCulturesBySideId(sideIds);
            return cultures.ToLookup(c => c.ContentId);
        }

        public async Task<IDictionary<long, ShapeModel>> GetSingleShapesById(IEnumerable<long> ids, CancellationToken cancellationToken)
        {
            var shapes = await _shapeService.GetById(ids);
            return shapes.ToDictionary(c => c.Id);
        }

        public async Task<IDictionary<long, ShapeVersionModel>> GetSingleVersionsByShapeId(IEnumerable<long> shapeIds, CancellationToken cancellationToken)
        {
            var versions = await _shapeService.GetVersionsByShapeId(shapeIds);
            return versions.ToDictionary(c => c.ContentId);
        }

        public async Task<IDictionary<long, ShapeVersionModel>> GetActualVersionsByShapeId(IEnumerable<long> shapeIds, CancellationToken cancellationToken)
        {
            var versions = await _shapeService.GetActualVersions(shapeIds);
            return versions.ToDictionary(c => c.ContentId);
        }

        public async Task<IDictionary<long, ShapeSideModel>> GetSingleSidesByVersionId(IEnumerable<long> versionIds, CancellationToken cancellationToken)
        {
            var sides = await _shapeService.GetSidesByVersionId(versionIds);
            return sides.ToDictionary(c => c.Id);
        }

        public async Task<IDictionary<(long, string), ShapeVersionCultureModel>> GetSingleVersionCultures(IEnumerable<(long, string)> keys, CancellationToken cancellationToken)
        {
            var cultures = await _shapeService.GetVersionCultures(keys);
            return cultures.ToDictionary(c => (c.ContentId, c.Culture));
        }

        public async Task<IDictionary<(long, string), ShapeSideCultureModel>> GetSingleSideCultures(IEnumerable<(long, string)> keys, CancellationToken cancellationToken)
        {
            var cultures = await _shapeService.GetSideCultures(keys);
            return cultures.ToDictionary(c => (c.ContentId, c.Culture));
        }
    }
}
