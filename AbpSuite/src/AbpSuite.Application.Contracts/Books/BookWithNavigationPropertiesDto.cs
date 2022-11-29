using AbpSuite.Authors;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace AbpSuite.Books
{
    public class BookWithNavigationPropertiesDto
    {
        public BookDto Book { get; set; }

        public AuthorDto Author { get; set; }

    }
}