using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using AbpSuite.EntityFrameworkCore;

namespace AbpSuite.Authors
{
    public class EfCoreAuthorRepository : EfCoreRepository<AbpSuiteDbContext, Author, Guid>, IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<AbpSuiteDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Author>> GetListAsync(
            string filterText = null,
            string sureName = null,
            int? ageMin = null,
            int? ageMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, sureName, ageMin, ageMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AuthorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string sureName = null,
            int? ageMin = null,
            int? ageMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, sureName, ageMin, ageMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Author> ApplyFilter(
            IQueryable<Author> query,
            string filterText,
            string sureName = null,
            int? ageMin = null,
            int? ageMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.SureName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(sureName), e => e.SureName.Contains(sureName))
                    .WhereIf(ageMin.HasValue, e => e.Age >= ageMin.Value)
                    .WhereIf(ageMax.HasValue, e => e.Age <= ageMax.Value);
        }
    }
}