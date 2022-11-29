using AbpSuite.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace AbpSuite.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpSuiteEntityFrameworkCoreModule),
    typeof(AbpSuiteApplicationContractsModule)
)]
public class AbpSuiteDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });
    }
}
