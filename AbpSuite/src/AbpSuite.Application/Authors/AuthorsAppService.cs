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
using AbpSuite.Authors;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using AbpSuite.Shared;

namespace AbpSuite.Authors
{

    [Authorize(AbpSuitePermissions.Authors.Default)]
    public class AuthorsAppService : ApplicationService, IAuthorsAppService
    {
        private readonly IDistributedCache<AuthorExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public AuthorsAppService(IAuthorRepository authorRepository, AuthorManager authorManager, IDistributedCache<AuthorExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        public virtual async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorsInput input)
        {
            var totalCount = await _authorRepository.GetCountAsync(input.FilterText, input.SureName, input.AgeMin, input.AgeMax);
            var items = await _authorRepository.GetListAsync(input.FilterText, input.SureName, input.AgeMin, input.AgeMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AuthorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Author>, List<AuthorDto>>(items)
            };
        }

        public virtual async Task<AuthorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Author, AuthorDto>(await _authorRepository.GetAsync(id));
        }

        [Authorize(AbpSuitePermissions.Authors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        [Authorize(AbpSuitePermissions.Authors.Create)]
        public virtual async Task<AuthorDto> CreateAsync(AuthorCreateDto input)
        {

            var author = await _authorManager.CreateAsync(
            input.SureName, input.Age
            );

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [Authorize(AbpSuitePermissions.Authors.Edit)]
        public virtual async Task<AuthorDto> UpdateAsync(Guid id, AuthorUpdateDto input)
        {

            var author = await _authorManager.UpdateAsync(
            id,
            input.SureName, input.Age, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AuthorExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _authorRepository.GetListAsync(input.FilterText, input.SureName, input.AgeMin, input.AgeMax);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Author>, List<AuthorExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Authors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new AuthorExcelDownloadTokenCacheItem { Token = token },
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