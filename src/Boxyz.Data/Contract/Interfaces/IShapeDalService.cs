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

        Task<ShapeFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<ShapeSideFlatModel>> GetFlatSides(long shapeVersionId, string culture);
    }
}
