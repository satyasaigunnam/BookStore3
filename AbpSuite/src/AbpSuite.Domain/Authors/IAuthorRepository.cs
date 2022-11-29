using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AbpSuite.Authors
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        Task<List<Author>> GetListAsync(
            string filterText = null,
            string sureName = null,
            int? ageMin = null,
            int? ageMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string sureName = null,
            int? ageMin = null,
            int? ageMax = null,
            CancellationToken cancellationToken = default);
    }
}