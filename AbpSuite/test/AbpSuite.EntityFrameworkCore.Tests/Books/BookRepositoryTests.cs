using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using AbpSuite.Books;
using AbpSuite.EntityFrameworkCore;
using Xunit;

namespace AbpSuite.Books
{
    public class BookRepositoryTests : AbpSuiteEntityFrameworkCoreTestBase
    {
        private readonly IBookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _bookRepository = GetRequiredService<IBookRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetListAsync(
                    title: "56a079ddc7e94f63833ca2801"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetCountAsync(
                    title: "9d30d2c1b8b349a5b7ccb4091"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}