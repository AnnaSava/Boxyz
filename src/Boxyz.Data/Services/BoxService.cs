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
    public class BoxService : BaseService, IBoxService
    {
        public BoxService(BoxDbContext dbContext, IMapper mapper)
             : base(dbContext, mapper)
        {

        }

        public async Task<BoxModel> GetOne(long id)
        {
            var entity = await _dbContext.Boxes
                .Where(m => m.Id == id)
                .Include(m => m.Shape)
                .Include(m => m.Versions).ThenInclude(m => m.Sides).ThenInclude(m => m.Cultures)
                .Include(m => m.Versions).ThenInclude(m => m.Sides).ThenInclude(m => m.ShapeSide)
                .Include(m => m.Versions).ThenInclude(m => m.ShapeVersion)
                .FirstOrDefaultAsync();

            return _mapper.Map<BoxModel>(entity);
        }
    }
}
