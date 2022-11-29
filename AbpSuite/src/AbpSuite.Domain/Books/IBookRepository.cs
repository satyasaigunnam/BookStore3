using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace AbpSuite.Books
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        Task<BookWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<BookWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string title = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? authorId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Book>> GetListAsync(
                    string filterText = null,
                    string title = null,
                    int? yearMin = null,
                    int? yearMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string title = null,
            int? yearMin = null,
            int? yearMax = null,
            Guid? authorId = null,
            CancellationToken cancellationToken = default);
    }
}