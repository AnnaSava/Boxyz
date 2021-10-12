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
    public class BoxService : BaseService, IBoxService
    {
        public BoxService(BoxContext dbContext, IMapper mapper)
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

        public async Task<IEnumerable<BoxVersionModel>> GetVersionsByBoxId(long boxId)
        {
            return await _dbContext.BoxVersions
                .Where(m => m.ContentId == boxId)
                .ProjectTo<BoxVersionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<BoxVersionModel>> GetVersionsByBoxId(IEnumerable<long> boxIds)
        {
            return await _dbContext.BoxVersions
                .Where(m => boxIds.Contains(m.ContentId))
                .ProjectTo<BoxVersionModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BoxVersionModel> GetActualVersion(long boxId)
        {
            var entity = await _dbContext.BoxVersions
                .Where(m => m.ContentId == boxId)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            return _mapper.Map<BoxVersionModel>(entity);
        }

        public async Task<IEnumerable<BoxVersionModel>> GetActualVersions(IEnumerable<long> boxIds)
        {
            return await _dbContext.GetActualVersions<BoxVersion, BoxVersionModel>(boxIds, _mapper);
        }

        public async Task<IEnumerable<BoxSideModel>> GetSidesByVersionId(long versionId)
        {
            return await _dbContext.BoxSides
                .Where(m => m.BoxVersionId == versionId)
                .ProjectTo<BoxSideModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<BoxSideModel>> GetSidesByVersionId(IEnumerable<long> versionIds)
        {
            return await _dbContext.BoxSides
                .Where(m => versionIds.Contains(m.BoxVersionId))
                .ProjectTo<BoxSideModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<BoxSideCultureModel>> GetSideCulturesBySideId(long sideId)
        {
            return await _dbContext.BoxSideCultures
                .Where(m => m.ContentId == sideId)
                .ProjectTo<BoxSideCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<BoxSideCultureModel>> GetSideCulturesBySideId(IEnumerable<long> sideIds)
        {
            return await _dbContext.BoxSideCultures
                .Where(m => sideIds.Contains(m.ContentId))
                .ProjectTo<BoxSideCultureModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BoxSideCultureModel> GetSideCulture(long sideId, string culture)
        {
            var entity = await _dbContext.BoxSideCultures
                .Where(m => m.ContentId == sideId && m.Culture == culture)
                .FirstOrDefaultAsync();

            return _mapper.Map<BoxSideCultureModel>(entity);
        }

        public async Task<IEnumerable<BoxSideCultureModel>> GetSideCultures(IEnumerable<(long, string)> keys)
        {
            return await _dbContext.GetCultures<BoxSideCulture, BoxSideCultureModel>(keys, _mapper);
        }

        public async Task<BoxFlatModel> GetFlat(long id, string culture)
        {
            var entity = await _dbContext.Boxes
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var actualVersion = await _dbContext.BoxVersions
                .Where(m => m.ContentId == id)
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
                    c => c.ContentId,
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
