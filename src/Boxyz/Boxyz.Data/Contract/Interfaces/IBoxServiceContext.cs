using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Contract
{
    public interface IBoxServiceContext
    {
        IBoxShapeBoardService ShapeBoardService { get; }

        IBoxShapeService ShapeService { get; }

        IBoxService BoxService { get; }
    }
}
