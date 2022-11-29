using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace AbpSuite.Authors
{
    public class AuthorManager : DomainService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> CreateAsync(
        string sureName, int age)
        {
            var author = new Author(
             GuidGenerator.Create(),
             sureName, age
             );

            return await _authorRepository.InsertAsync(author);
        }

        public async Task<Author> UpdateAsync(
            Guid id,
            string sureName, int age, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _authorRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var author = await AsyncExecuter.FirstOrDefaultAsync(query);

            author.SureName = sureName;
            author.Age = age;

            author.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _authorRepository.UpdateAsync(author);
        }

    }
}