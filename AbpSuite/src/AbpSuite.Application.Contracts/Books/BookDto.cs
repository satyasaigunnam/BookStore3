using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace AbpSuite.Books
{
    public class BookDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public Guid? AuthorId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}