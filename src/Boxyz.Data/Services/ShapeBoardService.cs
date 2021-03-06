using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boxyz.Proto.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boxyz.Proto.Data.Services
{
    public class ShapeBoardService : BaseService, IShapeBoardService
    {
        public ShapeBoardService(BoxContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<ShapeBoardModel> Create(ShapeBoardInputModel model)
        {
            var entity = _mapper.Map<ShapeBoard>(model);

            _dbContext.ShapeBoards.Add(entity);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<ShapeBoardModel>(entity);
        }

        public async Task<ShapeBoardModel> GetOne(long id)
        {
            var entity = await _dbContext.ShapeBoards
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeBoardModel>(entity);
        }

        public async Task<ShapeBoardFlatModel> GetFlat(long id, string culture)
        {
            var entity = await _dbContext.ShapeBoards
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var cultureEntity = await _dbContext.ShapeBoardCultures
                .Where(m => m.ContentId == id && m.Culture == culture)
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

        public async Task<IEnumerable<ShapeBoardCultureModel>> GetCulturesByBoardId(long boardId)
        {
            return await _dbContext.ShapeBoardCultures
                .Where(m => m.ContentId == boardId)
                .ProjectTo<ShapeBoardCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeBoardCultureModel>> GetCulturesByBoardId(IEnumerable<long> boardIds)
        {
            return await _dbContext.ShapeBoardCultures
                .Where(m => boardIds.Contains(m.ContentId))
                .ProjectTo<ShapeBoardCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeBoardCultureModel> GetCulture(long boardId, string culture)
        {
            var entity = await _dbContext.ShapeBoardCultures
                .Where(m => m.ContentId == boardId && m.Culture == culture)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeBoardCultureModel>(entity);
        }

        public async Task<IEnumerable<ShapeBoardCultureModel>> GetCultures(IEnumerable<(long, string)> keys)
        {
            return await _dbContext.GetCultures<ShapeBoardCulture, ShapeBoardCultureModel>(keys, _mapper);
        }

        public async Task<IEnumerable<ShapeBoardModel>> GetAll(int page, int count)
        {
            return await _dbContext.ShapeBoards
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Skip((page - 1) * count)
                .Take(count)
                .ProjectTo<ShapeBoardModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeBoardModel>> GetManyByIds(IEnumerable<long> ids)
        {
            return await _dbContext.ShapeBoards
                .AsNoTracking()
                .OrderBy(m => m.Id)
                .Where(m => ids.Contains(m.Id))
                .ProjectTo<ShapeBoardModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeBoardFlatModel>> GetAllFlat(int page, int count, string culture)
        {
            return await _dbContext.ShapeBoards
                .Join(_dbContext.ShapeBoardCultures.Where(m => m.Culture == culture),
                b => b.Id,
                c => c.ContentId,
                (b, c) => new ShapeBoardFlatModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Level = b.Level,
                    Path = b.Path,
                    Title = c.Title,
                    Culture = culture
                })
                .OrderBy(m => m.Id)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
        }
    }
}
