using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IShapeBoardDalService
    {
        Task<ShapeBoardModel> Create(ShapeBoardModel model);

        Task<ShapeBoardModel> GetOne(long Id);

        Task<ShapeBoardFlatModel> GetFlat(long id, string culture);

        Task<IEnumerable<ShapeBoardCultureModel>> GetCultures(long boardId);

        Task<ShapeBoardCultureModel> GetCulture(long boardId, string culture);

        Task<IEnumerable<ShapeBoardModel>> GetAll(int page, int count);

        Task<IEnumerable<ShapeBoardFlatModel>> GetAllFlat(int page, int count, string culture);
    }
}
