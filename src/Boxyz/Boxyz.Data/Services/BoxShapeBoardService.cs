using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Services
{
    public class BoxShapeBoardService : BaseService, IBoxShapeBoardService
    {
        public BoxShapeBoardService(BoxDbContext dbContext, IMapper mapper) : base (dbContext, mapper)
        {

        }

        public async Task<BoxShapeBoardModel> Create(BoxShapeBoardModel model)
        {
            var entity = _mapper.Map<BoxShapeBoard>(model);

            _dbContext.BoxShapeBoards.Add(entity);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<BoxShapeBoardModel> GetOne(long id)
        {
            var entity = await _dbContext.BoxShapeBoards
                .Where(m => m.Id == id)
                .Include(m => m.Cultures)
                .FirstOrDefaultAsync();

            return _mapper.Map<BoxShapeBoardModel>(entity);
        }

        public async Task<IEnumerable<BoxShapeBoardCultureModel>> GetCultures(long boardId)
        {
            return await _dbContext.BoxShapeBoardCultures
                .Where(m => m.BoardId == boardId)                
                .ProjectTo<BoxShapeBoardCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
