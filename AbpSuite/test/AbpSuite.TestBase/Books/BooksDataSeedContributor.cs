using AbpSuite.Authors;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using AbpSuite.Books;

namespace AbpSuite.Books
{
    public class BooksDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AuthorsDataSeedContributor _authorsDataSeedContributor;

        public BooksDataSeedContributor(IBookRepository bookRepository, IUnitOfWorkManager unitOfWorkManager, AuthorsDataSeedContributor authorsDataSeedContributor)
        {
            _bookRepository = bookRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _authorsDataSeedContributor = authorsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _authorsDataSeedContributor.SeedAsync(context);

            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("9d5fda9b-3911-452d-b3d3-87abf502f055"),
                title: "56a079ddc7e94f63833ca2801",
                year: 55,
                authorId: null
            ));

            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("5509b8ef-c288-4df7-9269-c417a1b6156b"),
                title: "9d30d2c1b8b349a5b7ccb4091",
                year: 39,
                authorId: null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}