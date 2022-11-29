using AbpSuite.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace AbpSuite.Web.Pages;

public abstract class AbpSuitePageModel : AbpPageModel
{
    protected AbpSuitePageModel()
    {
        LocalizationResourceType = typeof(AbpSuiteResource);
    }
}
