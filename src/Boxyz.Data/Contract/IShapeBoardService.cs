using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data
{
    public interface IShapeBoardService
    {
        Task<ShapeBoardModel> Create(ShapeBoardInputModel model);

        Task<ShapeBoardModel> GetOne(long Id);

        Task<ShapeBoardFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<ShapeBoardCultureModel>> GetCulturesByBoardId(long boardId);

        Task<IEnumerable<ShapeBoardCultureModel>> GetCulturesByBoardId(IEnumerable<long> boardIds);

        Task<IEnumerable<ShapeBoardCultureModel>> GetCultures(IEnumerable<(long, string)> keys);

        Task<ShapeBoardCultureModel> GetCulture(long boardId, string culture);

        Task<IEnumerable<ShapeBoardModel>> GetAll(int page, int count);

        Task<IEnumerable<ShapeBoardModel>> GetManyByIds(IEnumerable<long> ids);

        Task<IEnumerable<ShapeBoardFlatModel>> GetAllFlat(int page, int count, string culture);
    }
}
