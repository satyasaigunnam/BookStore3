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

namespace AbpSuite.Books
{
    public class EfCoreBookRepository : EfCoreRepository<AbpSuiteDbContext, Book, Guid>, IBookRepository
    {
        public EfCoreBookRepository(IDbContextProvider<AbpSuiteDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<BookWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(book => new BookWithNavigationProperties
                {
                    Book = book,
                    Author = dbContext.Authors.FirstOrDefault(c => c.Id == book.AuthorId)
                }).FirstOrDefault();
        }

        public async Task<List<BookWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string title = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? authorId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, title, yearMin, yearMax, authorId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BookConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<BookWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from book in (await GetDbSetAsync())
                   join author in (await GetDbContextAsync()).Authors on book.AuthorId equals author.Id into authors
                   from author in authors.DefaultIfEmpty()

                   select new BookWithNavigationProperties
                   {
                       Book = book,
                       Author = author
                   };
        }

        protected virtual IQueryable<BookWithNavigationProperties> ApplyFilter(
            IQueryable<BookWithNavigationProperties> query,
            string filterText,
            string title = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? authorId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Book.Title.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Book.Title.Contains(title))
                    .WhereIf(yearMin.HasValue, e => e.Book.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Book.Year <= yearMax.Value)
                    .WhereIf(authorId != null && authorId != Guid.Empty, e => e.Author != null && e.Author.Id == authorId);
        }

        public async Task<List<Book>> GetListAsync(
            string filterText = null,
            string title = null,
            int? yearMin = null,
            int? yearMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, title, yearMin, yearMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BookConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string title = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? authorId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, title, yearMin, yearMax, authorId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Book> ApplyFilter(
            IQueryable<Book> query,
            string filterText,
            string title = null,
            int? yearMin = null,
            int? yearMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Title.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(yearMin.HasValue, e => e.Year >= yearMin.Value)
                    .WhereIf(yearMax.HasValue, e => e.Year <= yearMax.Value);
        }
    }
}