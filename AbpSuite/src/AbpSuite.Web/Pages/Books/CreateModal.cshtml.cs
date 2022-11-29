using AbpSuite.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AbpSuite.Books;

namespace AbpSuite.Web.Pages.Books
{
    public class CreateModalModel : AbpSuitePageModel
    {
        [BindProperty]
        public BookCreateViewModel Book { get; set; }

        public List<SelectListItem> AuthorLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        private readonly IBooksAppService _booksAppService;

        public CreateModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            Book = new BookCreateViewModel();
            AuthorLookupList.AddRange((
                                    await _booksAppService.GetAuthorLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _booksAppService.CreateAsync(ObjectMapper.Map<BookCreateViewModel, BookCreateDto>(Book));
            return NoContent();
        }
    }

    public class BookCreateViewModel : BookCreateDto
    {
    }
}