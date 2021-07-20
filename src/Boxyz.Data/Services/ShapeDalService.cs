using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boxyz.Data.Services
{
    public class ShapeDalService : BaseService, IShapeDalService
    {
        public ShapeDalService(BoxDbContext dbContext, IMapper mapper)
             : base(dbContext, mapper)
        {

        }

        public async Task<ShapeModel> GetOne(long id)
        {
            var entity = await _dbContext.Shapes
                .Where(m => m.Id == id)
                .Include(m => m.Versions).ThenInclude(m => m.Cultures)
                .Include(m => m.Versions).ThenInclude(m => m.Sides).ThenInclude(m => m.Cultures)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeModel>(entity);
        }

        public async Task<IEnumerable<ShapeModel>> GetById(IEnumerable<long> ids)
        {
            return await _dbContext.Shapes
                .Where(m => ids.Contains(m.Id))
                .ProjectTo<ShapeModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeVersionModel>> GetVersionsByShapeId(long shapeId)
        {
            return await _dbContext.ShapeVersions
                .Where(m => m.ContentId == shapeId)
                .ProjectTo<ShapeVersionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeVersionModel>> GetVersionsByShapeId(IEnumerable<long> shapeIds)
        {
            return await _dbContext.ShapeVersions
                .Where(m => shapeIds.Contains(m.ContentId))
                .ProjectTo<ShapeVersionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeVersionModel> GetActualVersion(long shapeId)
        {
            var entity = await _dbContext.ShapeVersions
                .Where(m => m.ContentId == shapeId)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeVersionModel>(entity);
        }

        public async Task<IEnumerable<ShapeVersionModel>> GetActualVersions(IEnumerable<long> shapeIds)
        {
            return await _dbContext.GetActualVersions<ShapeVersion, ShapeVersionModel>(shapeIds, _mapper);
        }

        public async Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCulturesByVersionId(long versionId)
        {
            return await _dbContext.ShapeVersionCultures
                .Where(m => m.ContentId == versionId)
                .ProjectTo<ShapeVersionCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCulturesByVersionId(IEnumerable<long> versionIds)
        {
            return await _dbContext.ShapeVersionCultures
                .Where(m => versionIds.Contains(m.ContentId))
                .ProjectTo<ShapeVersionCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeVersionCultureModel> GetVersionCulture(long versionId, string culture)
        {
            var entity = await _dbContext.ShapeVersionCultures
                .Where(m => m.ContentId == versionId && m.Culture == culture)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeVersionCultureModel>(entity);
        }

        public async Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCultures(IEnumerable<(long, string)> keys)
        {
            return await _dbContext.GetCultures<ShapeVersionCulture, ShapeVersionCultureModel>(keys, _mapper);
        }

        public async Task<IEnumerable<ShapeSideModel>> GetSidesByVersionId(long versionId)
        {
            return await _dbContext.ShapeSides
                .Where(m => m.ShapeVersionId == versionId)
                .AsNoTracking()
                .ProjectTo<ShapeSideModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeSideModel>> GetSidesByVersionId(IEnumerable<long> versionIds)
        {
            return await _dbContext.ShapeSides
                .Where(c => versionIds.Contains(c.ShapeVersionId))
                .AsNoTracking()
                .ProjectTo<ShapeSideModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeSideModel> GetSide(long sideId)
        {
            return await _dbContext.ShapeSides
                .Where(m => m.Id == sideId)
                .ProjectTo<ShapeSideModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ShapeSideCultureModel>> GetSideCulturesBySideId(long sideId)
        {
            return await _dbContext.ShapeSideCultures
                .Where(m => m.ContentId == sideId)
                .ProjectTo<ShapeSideCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds)
        {
            return await _dbContext.ShapeSideCultures
                .Where(m => sideIds.Contains(m.ContentId))
                .ProjectTo<ShapeSideCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeSideCultureModel> GetSideCulture(long sideId, string culture)
        {
            var entity = await _dbContext.ShapeSideCultures
                .Where(m => m.ContentId == sideId && m.Culture == culture)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeSideCultureModel>(entity);
        }

        public async Task<IEnumerable<ShapeSideCultureModel>> GetSideCultures(IEnumerable<(long, string)> keys)
        {
            return await _dbContext.GetCultures<ShapeSideCulture, ShapeSideCultureModel>(keys, _mapper);
        }

        public async Task<ShapeFlatModel> GetFlat(long id, string culture)
        {
            var entity = await _dbContext.Shapes
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var actualVersion = await _dbContext.ShapeVersions
                .Where(m => m.ContentId == id)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            var versionCulture = await _dbContext.ShapeVersionCultures
                .Where(m => m.ContentId == actualVersion.Id && m.Culture == culture)
                .FirstOrDefaultAsync();

            return new ShapeFlatModel
            {
                Id = entity.Id,
                IsApproved = actualVersion.IsApproved,
                ConstName = entity.ConstName,
                Created = actualVersion.Created,
                LastUpdated = entity.LastUpdated,
                Culture = culture,
                Title = versionCulture.Title,
                VersionId = actualVersion.Id
            };
        }

        public async Task<IEnumerable<ShapeSideFlatModel>> GetFlatSides(long shapeVersionId, string culture)
        {
            var sides = await _dbContext.ShapeSides.Where(m => m.ShapeVersionId == shapeVersionId)
                .Join(_dbContext.ShapeSideCultures.Where(m => m.Culture == culture),
                s => s.Id,
                c => c.ContentId,
                (s, c) => new ShapeSideFlatModel
                {
                    Id = s.Id,
                    ConstName = s.ConstName,
                    DataType = s.DataType,
                    Culture = c.Culture,
                    Title = c.Title
                }).ToListAsync();
            
            return sides;
        }
    }
}
