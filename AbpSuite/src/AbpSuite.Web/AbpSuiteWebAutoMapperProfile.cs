using AbpSuite.Web.Pages.Books;
using AbpSuite.Books;
using AbpSuite.Web.Pages.Authors;
using Volo.Abp.AutoMapper;
using AbpSuite.Authors;
using AutoMapper;

namespace AbpSuite.Web;

public class AbpSuiteWebAutoMapperProfile : Profile
{
    public AbpSuiteWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<AuthorDto, AuthorUpdateViewModel>();
        CreateMap<AuthorUpdateViewModel, AuthorUpdateDto>();
        CreateMap<AuthorCreateViewModel, AuthorCreateDto>();

        CreateMap<BookDto, BookUpdateViewModel>();
        CreateMap<BookUpdateViewModel, BookUpdateDto>();
        CreateMap<BookCreateViewModel, BookCreateDto>();
    }
}