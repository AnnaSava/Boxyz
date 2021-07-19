using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IShapeDalService
    {
        Task<ShapeModel> GetOne(long Id);

        Task<IEnumerable<ShapeModel>> GetById(IEnumerable<long> ids);

        Task<IEnumerable<ShapeVersionModel>> GetVersionsByShapeId(long shapeId);

        Task<IEnumerable<ShapeVersionModel>> GetVersionsByShapeId(IEnumerable<long> shapeIds);

        Task<ShapeVersionModel> GetActualVersion(long shapeId);

        Task<IEnumerable<ShapeVersionModel>> GetActualVersions(IEnumerable<long> shapeIds);

        Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCulturesByVersionId(long versionId);

        Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCulturesByVersionId(IEnumerable<long> versionIds);

        Task<ShapeVersionCultureModel> GetVersionCulture(long versionId, string culture);

        Task<IEnumerable<ShapeSideModel>> GetSidesByVersionId(long versionId);

        Task<IEnumerable<ShapeSideModel>> GetSidesByVersionId(IEnumerable<long> versionIds);

        Task<ShapeSideModel> GetSide(long sideId);

        Task<IEnumerable<ShapeSideCultureModel>> GetSideCulturesBySideId(long sideId);

        Task<IEnumerable<ShapeSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds);

        Task<ShapeSideCultureModel> GetSideCulture(long sideId, string culture);

        Task<ShapeFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<ShapeSideFlatModel>> GetFlatSides(long shapeVersionId, string culture);
    }
}
