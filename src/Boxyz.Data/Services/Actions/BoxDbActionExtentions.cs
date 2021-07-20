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
    public static class BoxDbActionExtentions
    {
        public static async Task<IEnumerable<TModel>> GetCultures<TEntity, TModel>(this BoxDbContext dbContext, IEnumerable<(long, string)> keys, IMapper mapper)
            where TEntity : class, ICultureEntity
            where TModel : BaseCultureModel
        {
            var contentIds = keys.Select(k => k.Item1);
            var cultures = keys.Select(k => k.Item2);

            var rawSelection = await dbContext.Set<TEntity>()
                .Where(m => contentIds.Contains(m.ContentId) && cultures.Contains(m.Culture))
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            return rawSelection.Join(keys,
                  s => new { s.ContentId, s.Culture },
                  k => new { ContentId = k.Item1, Culture = k.Item2 },
                  (s, k) => s);
        }

        // TODO: May be change the definition of what is actual
        // Or perhaps drop to raw SQL for this, as it’s a reasonably advanced use:

        //SELECT* FROM
        //(
        //  SELECT*, ROW_NUMBER() OVER(PARTITION BY strftime('%Y%m%d%H', ReportDate) ORDER BY ReportDate) rn
        // FROM WeatherReports
        //) WHERE rn = 1
        //https://www.tutorialguruji.com/c-sharp/first-could-not-be-translated-when-accessing-first-group-element-linq-groupby/amp/
        public static async Task<IEnumerable<TModel>> GetActualVersions<TEntity, TModel>(this BoxDbContext dbContext, IEnumerable<long> contentIds, IMapper mapper)
            where TEntity : class, IVersion
        {
            var versions = await dbContext.Set<TEntity>()
                .Where(m => contentIds.Contains(m.ContentId))
                .ToListAsync();

            // So sad...
            var v = versions.GroupBy(m => m.ContentId)
                .Select(m => m.OrderByDescending(s => s.Created).First())
                .AsQueryable()
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToList();

            return v;
        }
    }
}
