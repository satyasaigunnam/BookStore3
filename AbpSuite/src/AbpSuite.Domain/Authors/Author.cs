using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace AbpSuite.Authors
{
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string SureName { get; set; }

        public virtual int Age { get; set; }

        public Author()
        {

        }

        public Author(Guid id, string sureName, int age)
        {

            Id = id;
            Check.NotNull(sureName, nameof(sureName));
            Check.Length(sureName, nameof(sureName), AuthorConsts.SureNameMaxLength, AuthorConsts.SureNameMinLength);
            if (age < AuthorConsts.AgeMinLength)
            {
                throw new ArgumentOutOfRangeException(nameof(age), age, "The value of 'age' cannot be lower than " + AuthorConsts.AgeMinLength);
            }

            if (age > AuthorConsts.AgeMaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(age), age, "The value of 'age' cannot be greater than " + AuthorConsts.AgeMaxLength);
            }

            SureName = sureName;
            Age = age;
        }

    }
}