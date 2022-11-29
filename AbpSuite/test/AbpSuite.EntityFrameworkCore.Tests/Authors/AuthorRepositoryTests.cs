using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using AbpSuite.Authors;
using AbpSuite.EntityFrameworkCore;
using Xunit;

namespace AbpSuite.Authors
{
    public class AuthorRepositoryTests : AbpSuiteEntityFrameworkCoreTestBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorRepositoryTests()
        {
            _authorRepository = GetRequiredService<IAuthorRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _authorRepository.GetListAsync(
                    sureName: "68bbd314bd5c4e738e4cb1bf1"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _authorRepository.GetCountAsync(
                    sureName: "05b16c0d0577406982aa24509"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}