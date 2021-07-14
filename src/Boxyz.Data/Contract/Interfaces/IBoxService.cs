using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IBoxService
    {
        Task<BoxModel> GetOne(long Id);

        Task<BoxFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<BoxSideFlatModel>> GetFlatSides(long boxVersionId, string culture);
    }
}
