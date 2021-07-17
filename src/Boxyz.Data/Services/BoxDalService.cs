using AutoMapper;
using AutoMapper.QueryableExtensions;
using Boxyz.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Services
{
    public class BoxDalService : BaseService, IBoxDalService
    {
        public BoxDalService(BoxDbContext dbContext, IMapper mapper)
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

        public async Task<IEnumerable<BoxVersionModel>> GetVersions(long boxId)
        {
            return await _dbContext.BoxVersions
                .Where(m => m.BoxId == boxId)
                .ProjectTo<BoxVersionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BoxVersionModel> GetActualVersion(long boxId)
        {
            var entity = await _dbContext.BoxVersions
                .Where(m => m.BoxId == boxId)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            return _mapper.Map<BoxVersionModel>(entity);
        }

        public async Task<IEnumerable<BoxSideModel>> GetSides(long versionId)
        {
            return await _dbContext.BoxSides
                .Where(m => m.BoxVersionId == versionId)
                .ProjectTo<BoxSideModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<BoxSideCultureModel>> GetSideCultures(long sideId)
        {
            return await _dbContext.BoxSideCultures
                .Where(m => m.BoxSideId == sideId)
                .ProjectTo<BoxSideCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BoxSideCultureModel> GetSideCulture(long sideId, string culture)
        {
            var entity = await _dbContext.BoxSideCultures
                .Where(m => m.BoxSideId == sideId && m.Culture == culture)
                .FirstOrDefaultAsync();

            return _mapper.Map<BoxSideCultureModel>(entity);
        }

        public async Task<BoxFlatModel> GetFlat(long id, string culture)
        {
            var entity = await _dbContext.Boxes
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var actualVersion = await _dbContext.BoxVersions
                .Where(m => m.BoxId == id)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            return new BoxFlatModel
            {
                Id = entity.Id,
                Created = actualVersion.Created,
                IsApproved = actualVersion.IsApproved,
                ShapeId = entity.ShapeId,
                VersionId = actualVersion.Id,
                Culture = culture,
                ShapeVersionId = actualVersion.ShapeVersionId
            };
        }

        public async Task<IEnumerable<BoxSideFlatModel>> GetFlatSides(long boxVersionId, string culture)
        {
            //var sides = await _dbContext.BoxSides.Where(m => m.BoxVersionId == boxVersionId)
            //    .GroupJoin(_dbContext.BoxSideCultures.Where(m => m.Culture == culture),
            //        s => s.Id,
            //        c => c.BoxSideId,
            //        (s, c) => new { Side = s, Culture = c })
            //    .SelectMany(sc => sc.Culture.DefaultIfEmpty(),
            //        (x, y) => new { Side = x.Side, Culture = y })
            //    .ToListAsync();

            //return
            //    sides
            //    .Select(m => new BoxSideFlatModel
            //    {
            //        Id = m.Side.Id,
            //        UniversalValue = m.Side.UniversalValue,
            //        Value = m.Culture == null ? "" : m.Culture.Value,
            //        Culture = culture,
            //        ShapeSideId = m.Side.ShapeSideId
            //    });


            return await _dbContext.BoxSides.Where(m => m.BoxVersionId == boxVersionId)
                .GroupJoin(_dbContext.BoxSideCultures.Where(m => m.Culture == culture),
                    s => s.Id,
                    c => c.BoxSideId,
                    (s, c) => new { Side = s, Culture = c })
                .SelectMany(sc => sc.Culture.DefaultIfEmpty(),
                    (x, y) => new { Side = x.Side, Culture = y })
                .Select(m => new BoxSideFlatModel
                {
                    Id = m.Side.Id,
                    UniversalValue = m.Side.UniversalValue,
                    Value = m.Culture == null ? "" : m.Culture.Value,
                    Culture = culture,
                    ShapeSideId = m.Side.ShapeSideId
                })
                .ToListAsync();


            //var sides = await _dbContext.BoxSides.Where(m => m.BoxVersionId == boxVersionId)
            //    .Join(_dbContext.BoxSideCultures.Where(m => m.Culture == culture),
            //    s => s.Id,
            //    c => c.BoxSideId,
            //    (s, c) => new BoxSideFlatModel
            //    {
            //        Id = s.Id,
            //        UniversalValue = s.UniversalValue,
            //        Culture = c.Culture,
            //        ShapeSideId = s.ShapeSideId
            //    }).ToListAsync();

            //return sides;
        }
    }
}
