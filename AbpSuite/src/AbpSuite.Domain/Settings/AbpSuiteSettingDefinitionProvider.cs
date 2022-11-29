using Volo.Abp.Settings;

namespace AbpSuite.Settings;

public class AbpSuiteSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AbpSuiteSettings.MySetting1));
    }
}
