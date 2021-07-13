using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IShapeBoardService
    {
        Task<ShapeBoardModel> Create(ShapeBoardModel model);

        Task<ShapeBoardModel> GetOne(long Id);

        Task<IEnumerable<ShapeBoardCultureModel>> GetCultures(long boardId);
    }
}
