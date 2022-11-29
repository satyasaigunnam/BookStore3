using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace AbpSuite.Authors
{
    public class AuthorUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(AuthorConsts.SureNameMaxLength, MinimumLength = AuthorConsts.SureNameMinLength)]
        public string SureName { get; set; }
        [Required]
        [Range(AuthorConsts.AgeMinLength, AuthorConsts.AgeMaxLength)]
        public int Age { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}