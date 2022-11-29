using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace AbpSuite.Books
{
    public class BookManager : DomainService
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateAsync(
        Guid? authorId, string title, int year)
        {
            var book = new Book(
             GuidGenerator.Create(),
             authorId, title, year
             );

            return await _bookRepository.InsertAsync(book);
        }

        public async Task<Book> UpdateAsync(
            Guid id,
            Guid? authorId, string title, int year, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _bookRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var book = await AsyncExecuter.FirstOrDefaultAsync(query);

            book.AuthorId = authorId;
            book.Title = title;
            book.Year = year;

            book.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _bookRepository.UpdateAsync(book);
        }

    }
}