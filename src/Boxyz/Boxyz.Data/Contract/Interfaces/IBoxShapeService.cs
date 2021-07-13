using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IBoxShapeService
    {
        Task<BoxShapeModel> GetOne(long Id);
    }
}
