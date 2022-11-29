using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace AbpSuite.Authors
{
    public class AuthorsAppServiceTests : AbpSuiteApplicationTestBase
    {
        private readonly IAuthorsAppService _authorsAppService;
        private readonly IRepository<Author, Guid> _authorRepository;

        public AuthorsAppServiceTests()
        {
            _authorsAppService = GetRequiredService<IAuthorsAppService>();
            _authorRepository = GetRequiredService<IRepository<Author, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _authorsAppService.GetListAsync(new GetAuthorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("123f41c0-f231-4fc7-9243-0c2ce260ea41")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _authorsAppService.GetAsync(Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new AuthorCreateDto
            {
                SureName = "b9b55bcf10af4d89a0dabc7d0",
                Age = 34
            };

            // Act
            var serviceResult = await _authorsAppService.CreateAsync(input);

            // Assert
            var result = await _authorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SureName.ShouldBe("b9b55bcf10af4d89a0dabc7d0");
            result.Age.ShouldBe(34);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new AuthorUpdateDto()
            {
                SureName = "b36d3d00e18b43c6b905f41de",
                Age = 43
            };

            // Act
            var serviceResult = await _authorsAppService.UpdateAsync(Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc"), input);

            // Assert
            var result = await _authorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.SureName.ShouldBe("b36d3d00e18b43c6b905f41de");
            result.Age.ShouldBe(43);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _authorsAppService.DeleteAsync(Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc"));

            // Assert
            var result = await _authorRepository.FindAsync(c => c.Id == Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc"));

            result.ShouldBeNull();
        }
    }
}