using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace AbpSuite.Books
{
    public class BooksAppServiceTests : AbpSuiteApplicationTestBase
    {
        private readonly IBooksAppService _booksAppService;
        private readonly IRepository<Book, Guid> _bookRepository;

        public BooksAppServiceTests()
        {
            _booksAppService = GetRequiredService<IBooksAppService>();
            _bookRepository = GetRequiredService<IRepository<Book, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _booksAppService.GetListAsync(new GetBooksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Book.Id == Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055")).ShouldBe(true);
            result.Items.Any(x => x.Book.Id == Guid.Parse("5509b8ef-c288-4df7-9269-c417a1b6156b")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _booksAppService.GetAsync(Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BookCreateDto
            {
                Title = "287a0bca0de449a5b798c37e5",
                Year = 23
            };

            // Act
            var serviceResult = await _booksAppService.CreateAsync(input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe("287a0bca0de449a5b798c37e5");
            result.Year.ShouldBe(23);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BookUpdateDto()
            {
                Title = "df5e33b518804b05a6906b3ca",
                Year = 49
            };

            // Act
            var serviceResult = await _booksAppService.UpdateAsync(Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055"), input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe("df5e33b518804b05a6906b3ca");
            result.Year.ShouldBe(49);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _booksAppService.DeleteAsync(Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055"));

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055"));

            result.ShouldBeNull();
        }
    }
}