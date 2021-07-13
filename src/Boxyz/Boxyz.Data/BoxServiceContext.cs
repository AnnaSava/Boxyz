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
        public IBoxShapeBoardService ShapeBoardService { get; }

        public IBoxShapeService ShapeService { get; }

        public IBoxService BoxService { get; }

        public BoxServiceContext(IBoxShapeBoardService shapeBoardService,
            IBoxShapeService shapeService,
            IBoxService boxService)
        {
            ShapeBoardService = shapeBoardService;
            ShapeService = shapeService;
            BoxService = boxService;
        }
    }
}
