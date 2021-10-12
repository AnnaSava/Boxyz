using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Services
{
    public class BoxViewService : IBoxViewService
    {
        private readonly IBoxService _boxService;
        private readonly IShapeService _shapeService;
        private readonly IMapper _mapper;

        public BoxViewService(IBoxService boxService, IShapeService shapeService, IMapper mapper)
        {
            _boxService = boxService;
            _shapeService = shapeService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BoxSideFlatModel>> GetFlatBoxSides(long boxVersionId, long shapeVersionId, string culture)
        {
            var boxSides = await _boxService.GetFlatSides(boxVersionId, culture);
            var shapeSides = await _shapeService.GetFlatSides(shapeVersionId, culture);

            foreach (var boxSide in boxSides)
            {
                var shapeSide = shapeSides.FirstOrDefault(m => m.Id == boxSide.ShapeSideId);

                boxSide.Title = shapeSide.Title;
                boxSide.ConstName = shapeSide.ConstName;
                boxSide.DataType = shapeSide.DataType;
            }

            return boxSides;
        }

        public async Task<BoxObjectModel> GetBoxObject(long id, string culture)
        {
            var box = await _boxService.GetFlat(id, culture);
            var shape = await _shapeService.GetFlat(box.ShapeId, culture);
            var sides = await GetFlatBoxSides(box.VersionId, shape.VersionId, culture);

            var dict = new BoxObjectModel();

            dict.Add("id", box.Id);

            foreach (var side in sides)
            {
                object value;
                switch(side.DataType)
                {
                    case "year":
                    case "number": 
                        value = int.Parse(side.UniversalValue);
                        break;
                    default: value = side.Value;
                        break;
                }

                //dict.Add(side.ConstName, new ValueWithType { Value = side.Value ?? side.UniversalValue, Type = side.DataType });
                dict.Add(side.ConstName, value);
            }

            return dict;
        }
    }
}
