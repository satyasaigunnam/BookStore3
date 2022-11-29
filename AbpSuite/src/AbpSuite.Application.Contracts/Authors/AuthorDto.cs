using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace AbpSuite.Authors
{
    public class AuthorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string SureName { get; set; }
        public int Age { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}