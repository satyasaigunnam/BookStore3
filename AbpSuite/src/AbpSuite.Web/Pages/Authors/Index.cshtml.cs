using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using AbpSuite.Authors;
using AbpSuite.Shared;

namespace AbpSuite.Web.Pages.Authors
{
    public class IndexModel : AbpPageModel
    {
        public string SureNameFilter { get; set; }
        public int? AgeFilterMin { get; set; }

        public int? AgeFilterMax { get; set; }

        private readonly IAuthorsAppService _authorsAppService;

        public IndexModel(IAuthorsAppService authorsAppService)
        {
            _authorsAppService = authorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}