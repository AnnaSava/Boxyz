using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public interface IBoxService
    {
        Task<IEnumerable<BoxSideFlatModel>> GetFlatBoxSides(long boxVersionId, long shapeVersionId, string culture);

        Task<BoxObjectModel> GetBoxObject(long id, string culture);
    }
}
