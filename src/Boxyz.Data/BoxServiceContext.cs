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
        public IShapeBoardDalService ShapeBoardService { get; }

        public IShapeDalService ShapeService { get; }

        public IBoxDalService BoxService { get; }

        public BoxServiceContext(IShapeBoardDalService shapeBoardService,
            IShapeDalService shapeService,
            IBoxDalService boxService)
        {
            ShapeBoardService = shapeBoardService;
            ShapeService = shapeService;
            BoxService = boxService;
        }

        public async Task<IEnumerable<BoxSideFlatModel>> GetFlatBoxSides(long boxVersionId, long shapeVersionId, string culture)
        {
            var boxSides = await BoxService.GetFlatSides(boxVersionId, culture);
            var shapeSides = await ShapeService.GetFlatSides(shapeVersionId, culture);

            foreach (var boxSide in boxSides)
            {
                var shapeSide = shapeSides.FirstOrDefault(m => m.Id == boxSide.ShapeSideId);

                boxSide.Title = shapeSide.Title;
                boxSide.ConstName = shapeSide.ConstName;
                boxSide.DataType = shapeSide.DataType;
                
            }

            return boxSides;
        }
    }
}
