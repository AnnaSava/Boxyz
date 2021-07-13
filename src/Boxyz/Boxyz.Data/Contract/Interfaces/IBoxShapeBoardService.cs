using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IBoxShapeBoardService
    {
        Task<BoxShapeBoardModel> Create(BoxShapeBoardModel model);

        Task<BoxShapeBoardModel> GetOne(long Id);

        Task<IEnumerable<BoxShapeBoardCultureModel>> GetCultures(long boardId);
    }
}
