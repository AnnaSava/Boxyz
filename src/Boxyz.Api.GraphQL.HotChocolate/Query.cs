using Boxyz.Proto.Api.GraphQL.HotChocolate.DataLoaders;
using Boxyz.Proto.Api.GraphQL.HotChocolate.Types;
using Boxyz.Proto.Data;
using HotChocolate;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.HotChocolate
{
    public class Query
    {
        private IShapeBoardService _shapeBoardService;

        public Query(IShapeBoardService shapeBoardService)
        {
            _shapeBoardService = shapeBoardService;
        }

        public async Task<ShapeBoardType> ShapeBoard(long id)
        {
            var shapeBoard = await _shapeBoardService.GetOne(id);
            return new ShapeBoardType(shapeBoard);
        }

        public async Task<ShapeBoardType> ShapeBoardViaLoader(
        long id,
        ShapeBoardDataLoader dataLoader)
        => await dataLoader.LoadAsync(id);
    }
}
