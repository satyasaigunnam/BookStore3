using AbpSuite.Shared;
using AbpSuite.Authors;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AbpSuite.Permissions;
using AbpSuite.Books;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using AbpSuite.Shared;

namespace AbpSuite.Books
{

    [Authorize(AbpSuitePermissions.Books.Default)]
    public class BooksAppService : ApplicationService, IBooksAppService
    {
        private readonly IDistributedCache<BookExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IBookRepository _bookRepository;
        private readonly BookManager _bookManager;
        private readonly IRepository<Author, Guid> _authorRepository;

        public BooksAppService(IBookRepository bookRepository, BookManager bookManager, IDistributedCache<BookExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<Author, Guid> authorRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _bookRepository = bookRepository;
            _bookManager = bookManager; _authorRepository = authorRepository;
        }

        public virtual async Task<PagedResultDto<BookWithNavigationPropertiesDto>> GetListAsync(GetBooksInput input)
        {
            var totalCount = await _bookRepository.GetCountAsync(input.FilterText, input.Title, input.YearMin, input.YearMax, input.AuthorId);
            var items = await _bookRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Title, input.YearMin, input.YearMax, input.AuthorId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BookWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<BookWithNavigationProperties>, List<BookWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<BookWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<BookWithNavigationProperties, BookWithNavigationPropertiesDto>
                (await _bookRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<BookDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Book, BookDto>(await _bookRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetAuthorLookupAsync(LookupRequestDto input)
        {
            var query = (await _authorRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.SureName != null &&
                         x.SureName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Author>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Author>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        [Authorize(AbpSuitePermissions.Books.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        [Authorize(AbpSuitePermissions.Books.Create)]
        public virtual async Task<BookDto> CreateAsync(BookCreateDto input)
        {

            var book = await _bookManager.CreateAsync(
            input.AuthorId, input.Title, input.Year
            );

            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [Authorize(AbpSuitePermissions.Books.Edit)]
        public virtual async Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {

            var book = await _bookManager.UpdateAsync(
            id,
            input.AuthorId, input.Title, input.Year, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(BookExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _bookRepository.GetListAsync(input.FilterText, input.Title, input.YearMin, input.YearMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Book>, List<BookExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Books.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new BookExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}