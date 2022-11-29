using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using AbpSuite.Authors;

namespace AbpSuite.Authors
{
    public class AuthorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AuthorsDataSeedContributor(IAuthorRepository authorRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _authorRepository = authorRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _authorRepository.InsertAsync(new Author
            (
                id: Guid.Parse("b532c543-ec8c-4db9-b8e3-479dee9e82dc"),
                sureName: "68bbd314bd5c4e738e4cb1bf1",
                age: 46
            ));

            await _authorRepository.InsertAsync(new Author
            (
                id: Guid.Parse("123f41c0-f231-4fc7-9243-0c2ce260ea41"),
                sureName: "05b16c0d0577406982aa24509",
                age: 39
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}