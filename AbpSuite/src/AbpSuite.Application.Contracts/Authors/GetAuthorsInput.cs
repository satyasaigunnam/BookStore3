using Volo.Abp.Application.Dtos;
using System;

namespace AbpSuite.Authors
{
    public class GetAuthorsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string SureName { get; set; }
        public int? AgeMin { get; set; }
        public int? AgeMax { get; set; }

        public GetAuthorsInput()
        {

        }
    }
}