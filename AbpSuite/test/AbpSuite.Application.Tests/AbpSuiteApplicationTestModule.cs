using Volo.Abp.Modularity;

namespace AbpSuite;

[DependsOn(
    typeof(AbpSuiteApplicationModule),
    typeof(AbpSuiteDomainTestModule)
    )]
public class AbpSuiteApplicationTestModule : AbpModule
{

}
