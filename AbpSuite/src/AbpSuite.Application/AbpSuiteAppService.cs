using AbpSuite.Localization;
using Volo.Abp.Application.Services;

namespace AbpSuite;

/* Inherit your application services from this class.
 */
public abstract class AbpSuiteAppService : ApplicationService
{
    protected AbpSuiteAppService()
    {
        LocalizationResource = typeof(AbpSuiteResource);
    }
}
