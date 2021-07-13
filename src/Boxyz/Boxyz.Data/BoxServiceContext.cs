using Boxyz.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data
{
   public class BoxServiceContext : IBoxServiceContext
    {
        public IShapeBoardService ShapeBoardService { get; }

        public IShapeService ShapeService { get; }

        public IBoxService BoxService { get; }

        public BoxServiceContext(IShapeBoardService shapeBoardService,
            IShapeService shapeService,
            IBoxService boxService)
        {
            ShapeBoardService = shapeBoardService;
            ShapeService = shapeService;
            BoxService = boxService;
        }
    }
}
