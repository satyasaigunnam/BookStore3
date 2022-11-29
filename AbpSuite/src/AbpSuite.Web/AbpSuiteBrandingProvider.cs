using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace AbpSuite.Web;

[Dependency(ReplaceServices = true)]
public class AbpSuiteBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "AbpSuite";
}
