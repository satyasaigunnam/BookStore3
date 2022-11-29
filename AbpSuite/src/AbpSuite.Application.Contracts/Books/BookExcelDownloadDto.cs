using Volo.Abp.Application.Dtos;
using System;

namespace AbpSuite.Books
{
    public class BookExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Title { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public Guid? AuthorId { get; set; }

        public BookExcelDownloadDto()
        {

        }
    }
}