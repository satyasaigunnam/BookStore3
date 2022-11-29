using AbpSuite.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using AbpSuite.Authors;

namespace AbpSuite.Web.Pages.Authors
{
    public class EditModalModel : AbpSuitePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public AuthorUpdateViewModel Author { get; set; }

        private readonly IAuthorsAppService _authorsAppService;

        public EditModalModel(IAuthorsAppService authorsAppService)
        {
            _authorsAppService = authorsAppService;
        }

        public async Task OnGetAsync()
        {
            var author = await _authorsAppService.GetAsync(Id);
            Author = ObjectMapper.Map<AuthorDto, AuthorUpdateViewModel>(author);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _authorsAppService.UpdateAsync(Id, ObjectMapper.Map<AuthorUpdateViewModel, AuthorUpdateDto>(Author));
            return NoContent();
        }
    }

    public class AuthorUpdateViewModel : AuthorUpdateDto
    {
    }
}