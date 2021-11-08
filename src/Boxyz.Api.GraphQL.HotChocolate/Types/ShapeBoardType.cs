using Boxyz.Proto.Data;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.HotChocolate.Types
{
    [GraphQLName("ShapeBoard")]
    public class ShapeBoardType
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public string Path { get; set; }

        public ShapeBoardType(ShapeBoardModel shapeBoard)
        {
            Id = shapeBoard.Id;
            Name = shapeBoard.Name;
            Level = shapeBoard.Level;
            Path = shapeBoard.Path;
        }

        // TODO Переделать на DataLoader
        //public async Task<ShapeBoardCultureType> GetCulture(string culture, [Service] IShapeBoardService _shapeBoardService)
        //{
        //    var cultureModel = await _shapeBoardService.GetCulture(Id, culture);
        //    return new ShapeBoardCultureType(cultureModel);
        //}
    }
}
