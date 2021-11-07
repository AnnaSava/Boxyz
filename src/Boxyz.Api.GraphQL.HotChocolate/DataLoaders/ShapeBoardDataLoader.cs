using Boxyz.Proto.Api.GraphQL.HotChocolate.Types;
using Boxyz.Proto.Data;
using GreenDonut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Proto.Api.GraphQL.HotChocolate.DataLoaders
{
    public class ShapeBoardDataLoader : BatchDataLoader<long, ShapeBoardType>
    {
        private readonly IShapeBoardService _repository;

        public ShapeBoardDataLoader(
            IShapeBoardService repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _repository = repository;
        }

        protected override async Task<IReadOnlyDictionary<long, ShapeBoardType>> LoadBatchAsync(
            IReadOnlyList<long> keys,
            CancellationToken cancellationToken)
        {
            // instead of fetching one board, we fetch multiple boards
            var boards = await _repository.GetManyByIds(keys);
            return boards.Select(m => new ShapeBoardType(m)).ToDictionary(x => x.Id);
        }
    }
}
