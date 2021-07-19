using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boxyz.Data.Contract;
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
                .Where(m => m.ShapeId == shapeId)
                .ProjectTo<ShapeVersionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeVersionModel>> GetVersionsByShapeId(IEnumerable<long> shapeIds)
        {
            return await _dbContext.ShapeVersions
                .Where(m => shapeIds.Contains(m.ShapeId))
                .ProjectTo<ShapeVersionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeVersionModel> GetActualVersion(long shapeId)
        {
            var entity = await _dbContext.ShapeVersions
                .Where(m => m.ShapeId == shapeId)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeVersionModel>(entity);
        }

        // TODO: May be change the definition of what is actual
        // Or perhaps drop to raw SQL for this, as it’s a reasonably advanced use:

        //SELECT* FROM
        //(
        //  SELECT*, ROW_NUMBER() OVER(PARTITION BY strftime('%Y%m%d%H', ReportDate) ORDER BY ReportDate) rn
        // FROM WeatherReports
        //) WHERE rn = 1
        //https://www.tutorialguruji.com/c-sharp/first-could-not-be-translated-when-accessing-first-group-element-linq-groupby/amp/
        public async Task<IEnumerable<ShapeVersionModel>> GetActualVersions(IEnumerable<long> shapeIds)
        {
            var versions = await _dbContext.ShapeVersions
                .Where(m => shapeIds.Contains(m.ShapeId))
                .ToListAsync();              
                
            // So sad...
            var v = versions.GroupBy(m => m.ShapeId)
                .Select(m => m.OrderByDescending(s=>s.Created).First())
                .AsQueryable()
                .ProjectTo<ShapeVersionModel>(_mapper.ConfigurationProvider)
                .ToList();

            return v;
        }

        public async Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCulturesByVersionId(long versionId)
        {
            return await _dbContext.ShapeVersionCultures
                .Where(m => m.ShapeVersionId == versionId)
                .ProjectTo<ShapeVersionCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeVersionCultureModel>> GetVersionCulturesByVersionId(IEnumerable<long> versionIds)
        {
            return await _dbContext.ShapeVersionCultures
                .Where(m => versionIds.Contains(m.ShapeVersionId))
                .ProjectTo<ShapeVersionCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeVersionCultureModel> GetVersionCulture(long versionId, string culture)
        {
            var entity = await _dbContext.ShapeVersionCultures
                .Where(m => m.ShapeVersionId == versionId && m.Culture == culture)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeVersionCultureModel>(entity);
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
                .Where(m => m.ShapeSideId == sideId)
                .ProjectTo<ShapeSideCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShapeSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds)
        {
            return await _dbContext.ShapeSideCultures
                .Where(m => sideIds.Contains(m.ShapeSideId))
                .ProjectTo<ShapeSideCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShapeSideCultureModel> GetSideCulture(long sideId, string culture)
        {
            var entity = await _dbContext.ShapeSideCultures
                .Where(m => m.ShapeSideId == sideId && m.Culture == culture)
                .FirstOrDefaultAsync();

            return _mapper.Map<ShapeSideCultureModel>(entity);
        }

        public async Task<ShapeFlatModel> GetFlat(long id, string culture)
        {
            var entity = await _dbContext.Shapes
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var actualVersion = await _dbContext.ShapeVersions
                .Where(m => m.ShapeId == id)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            var versionCulture = await _dbContext.ShapeVersionCultures
                .Where(m => m.ShapeVersionId == actualVersion.Id && m.Culture == culture)
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
                c => c.ShapeSideId,
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
