using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public interface IBoxService
    {
        Task<BoxModel> GetOne(long Id);

        Task<IEnumerable<BoxVersionModel>> GetVersionsByBoxId(long boxId);

        Task<IEnumerable<BoxVersionModel>> GetVersionsByBoxId(IEnumerable<long> boxIds);

        Task<BoxVersionModel> GetActualVersion(long boxId);

        Task<IEnumerable<BoxVersionModel>> GetActualVersions(IEnumerable<long> boxIds);

        Task<IEnumerable<BoxSideModel>> GetSidesByVersionId(long versionId);

        Task<IEnumerable<BoxSideModel>> GetSidesByVersionId(IEnumerable<long> versionIds);

        Task<IEnumerable<BoxSideCultureModel>> GetSideCulturesBySideId(long sideId);

        Task<IEnumerable<BoxSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds);

        Task<BoxSideCultureModel> GetSideCulture(long sideId, string culture);

        Task<IEnumerable<BoxSideCultureModel>> GetSideCultures(IEnumerable<(long, string)> keys);

        Task<BoxFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<BoxSideFlatModel>> GetFlatSides(long boxVersionId, string culture);
    }
}
