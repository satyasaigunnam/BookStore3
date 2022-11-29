using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AbpSuite.Authors
{
    public class AuthorCreateDto
    {
        [Required]
        [StringLength(AuthorConsts.SureNameMaxLength, MinimumLength = AuthorConsts.SureNameMinLength)]
        public string SureName { get; set; }
        [Required]
        [Range(AuthorConsts.AgeMinLength, AuthorConsts.AgeMaxLength)]
        public int Age { get; set; }
    }
}