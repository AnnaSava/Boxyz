using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IBoxServiceContext
    {
        IShapeBoardDalService ShapeBoardService { get; }

        IShapeDalService ShapeService { get; }

        IBoxDalService BoxService { get; }

        Task<IEnumerable<BoxSideFlatModel>> GetFlatBoxSides(long boxVersionId, long shapeVersionId, string culture);
    }
}
