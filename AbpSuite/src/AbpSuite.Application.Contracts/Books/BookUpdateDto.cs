using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace AbpSuite.Books
{
    public class BookUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(BookConsts.TitleMaxLength, MinimumLength = BookConsts.TitleMinLength)]
        public string Title { get; set; }
        [Required]
        [Range(BookConsts.YearMinLength, BookConsts.YearMaxLength)]
        public int Year { get; set; }
        public Guid? AuthorId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}