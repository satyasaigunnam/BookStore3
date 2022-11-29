using AbpSuite.Authors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace AbpSuite.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Title { get; set; }

        public virtual int Year { get; set; }
        public Guid? AuthorId { get; set; }

        public Book()
        {

        }

        public Book(Guid id, Guid? authorId, string title, int year)
        {

            Id = id;
            Check.NotNull(title, nameof(title));
            Check.Length(title, nameof(title), BookConsts.TitleMaxLength, BookConsts.TitleMinLength);
            if (year < BookConsts.YearMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "The value of 'year' cannot be lower than " + BookConsts.YearMinLength);
            }

            if (year > BookConsts.YearMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(year), year, "The value of 'year' cannot be greater than " + BookConsts.YearMaxLength);
            }

            Title = title;
            Year = year;
            AuthorId = authorId;
        }

    }
}