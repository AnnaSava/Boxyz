using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IBoxServiceContext
    {
        IShapeBoardService ShapeBoardService { get; }

        IShapeService ShapeService { get; }

        IBoxService BoxService { get; }
    }
}
