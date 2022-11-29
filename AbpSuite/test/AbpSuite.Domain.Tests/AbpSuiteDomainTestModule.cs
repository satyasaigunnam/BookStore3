using AbpSuite.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace AbpSuite;

[DependsOn(
    typeof(AbpSuiteEntityFrameworkCoreTestModule)
    )]
public class AbpSuiteDomainTestModule : AbpModule
{

}
