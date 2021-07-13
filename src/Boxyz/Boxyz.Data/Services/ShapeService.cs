using AutoMapper;
using Boxyz.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Services
{
    public class ShapeService : BaseService, IShapeService
    {
        public ShapeService(BoxDbContext dbContext, IMapper mapper)
             : base(dbContext, mapper)
        {

        }

        public async Task<ShapeModel> GetOne(long id)
        {
            var entity = await _dbContext.BoxShapes
                .Where(m => m.Id == id)
                .Include(m => m.Versions).ThenInclude(m => m.Cultures)
                .Include(m => m.Versions).ThenInclude(m => m.Sides).ThenInclude(m => m.Cultures)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeModel>(entity);
        }
    }
}
