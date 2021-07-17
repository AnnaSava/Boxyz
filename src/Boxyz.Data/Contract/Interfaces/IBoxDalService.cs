using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IBoxDalService
    {
        Task<BoxModel> GetOne(long Id);

        Task<IEnumerable<BoxVersionModel>> GetVersions(long boxId);

        Task<BoxVersionModel> GetActualVersion(long boxId);

        Task<IEnumerable<BoxSideModel>> GetSides(long versionId);

        Task<IEnumerable<BoxSideCultureModel>> GetSideCultures(long sideId);

        Task<BoxSideCultureModel> GetSideCulture(long sideId, string culture);

        Task<BoxFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<BoxSideFlatModel>> GetFlatSides(long boxVersionId, string culture);
    }
}
