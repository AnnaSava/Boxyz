using AutoMapper;
using Boxyz.Data.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Services
{
    public class BoxService : IBoxService
    {
        private readonly IBoxDalService _boxDalService;
        private readonly IShapeDalService _shapeDalService;
        private readonly IMapper _mapper;

        public BoxService(IBoxDalService boxDalService, IShapeDalService shapeDalService, IMapper mapper)
        {
            _boxDalService = boxDalService;
            _shapeDalService = shapeDalService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BoxSideFlatModel>> GetFlatBoxSides(long boxVersionId, long shapeVersionId, string culture)
        {
            var boxSides = await _boxDalService.GetFlatSides(boxVersionId, culture);
            var shapeSides = await _shapeDalService.GetFlatSides(shapeVersionId, culture);

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
            var box = await _boxDalService.GetFlat(id, culture);
            var shape = await _shapeDalService.GetFlat(box.ShapeId, culture);
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
