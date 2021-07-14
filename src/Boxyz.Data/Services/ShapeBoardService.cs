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
    public class ShapeBoardService : BaseService, IShapeBoardService
    {
        public ShapeBoardService(BoxDbContext dbContext, IMapper mapper) : base (dbContext, mapper)
        {

        }

        public async Task<ShapeBoardModel> Create(ShapeBoardModel model)
        {
            var entity = _mapper.Map<ShapeBoard>(model);

            _dbContext.BoxShapeBoards.Add(entity);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task<ShapeBoardModel> GetOne(long id)
        {
            var entity = await _dbContext.BoxShapeBoards
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeBoardModel>(entity);
        }

        public async Task<ShapeBoardFlatModel> GetFlat(long id, string culture)
        {
            var entity = await _dbContext.BoxShapeBoards
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var cultureEntity = await _dbContext.BoxShapeBoardCultures
                .Where(m => m.BoardId == id && m.Culture == culture)
                .FirstOrDefaultAsync();

            return new ShapeBoardFlatModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Level = entity.Level,
                Path = entity.Path,
                Title = cultureEntity.Title,
                Culture = culture
            };
        }

        public async Task<IEnumerable<ShapeBoardCultureModel>> GetCultures(long boardId)
        {
            return await _dbContext.BoxShapeBoardCultures
                .Where(m => m.BoardId == boardId)                
                .ProjectTo<ShapeBoardCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
