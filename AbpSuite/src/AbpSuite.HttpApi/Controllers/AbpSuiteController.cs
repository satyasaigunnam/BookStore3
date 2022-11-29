using AbpSuite.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpSuite.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AbpSuiteController : AbpControllerBase
{
    protected AbpSuiteController()
    {
        LocalizationResource = typeof(AbpSuiteResource);
    }
}
