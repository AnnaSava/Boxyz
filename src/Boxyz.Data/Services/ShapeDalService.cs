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
    public class ShapeDalService : BaseService, IShapeDalService
    {
        public ShapeDalService(BoxDbContext dbContext, IMapper mapper)
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

        public async Task<ShapeFlatModel> GetFlat(long id, string culture)
        {
            var entity = await _dbContext.BoxShapes
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            var actualVersion = await _dbContext.BoxShapeVersions
                .Where(m => m.ShapeId == id)
                .OrderByDescending(m => m.Created)
                .FirstOrDefaultAsync();

            var versionCulture = await _dbContext.BoxShapeVersionCultures
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
            var sides = await _dbContext.BoxShapeSides.Where(m => m.ShapeVersionId == shapeVersionId)
                .Join(_dbContext.BoxShapeSideCultures.Where(m => m.Culture == culture),
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
