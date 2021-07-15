using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IShapeDalService
    {
        Task<ShapeModel> GetOne(long Id);

        Task<IEnumerable<ShapeVersionModel>> GetVersions(long shapeId);

        Task<ShapeVersionModel> GetActualVersion(long shapeId);

        Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCultures(long versionId);
        
        Task<ShapeVersionCultureModel> GetVersionCulture(long versionId, string culture);

        Task<IEnumerable<ShapeSideModel>> GetSides(long versionId);

        Task<IEnumerable<ShapeSideCultureModel>> GetSideCultures(long sideId);

        Task<ShapeSideCultureModel> GetSideCulture(long sideId, string culture);

        Task<ShapeFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<ShapeSideFlatModel>> GetFlatSides(long shapeVersionId, string culture);
    }
}
