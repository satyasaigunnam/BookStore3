using AbpSuite.Books;
using System;
using AbpSuite.Shared;
using Volo.Abp.AutoMapper;
using AbpSuite.Authors;
using AutoMapper;

namespace AbpSuite;

public class AbpSuiteApplicationAutoMapperProfile : Profile
{
    public AbpSuiteApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorExcelDto>();

        CreateMap<Book, BookDto>();
        CreateMap<Book, BookExcelDto>();
        CreateMap<BookWithNavigationProperties, BookWithNavigationPropertiesDto>();
        CreateMap<Author, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.SureName));
    }
}