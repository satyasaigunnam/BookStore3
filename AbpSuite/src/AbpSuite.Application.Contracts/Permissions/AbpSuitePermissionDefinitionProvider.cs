using AbpSuite.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpSuite.Permissions;

public class AbpSuitePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AbpSuitePermissions.GroupName);

        myGroup.AddPermission(AbpSuitePermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(AbpSuitePermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(AbpSuitePermissions.MyPermission1, L("Permission:MyPermission1"));

        var authorPermission = myGroup.AddPermission(AbpSuitePermissions.Authors.Default, L("Permission:Authors"));
        authorPermission.AddChild(AbpSuitePermissions.Authors.Create, L("Permission:Create"));
        authorPermission.AddChild(AbpSuitePermissions.Authors.Edit, L("Permission:Edit"));
        authorPermission.AddChild(AbpSuitePermissions.Authors.Delete, L("Permission:Delete"));

        var bookPermission = myGroup.AddPermission(AbpSuitePermissions.Books.Default, L("Permission:Books"));
        bookPermission.AddChild(AbpSuitePermissions.Books.Create, L("Permission:Create"));
        bookPermission.AddChild(AbpSuitePermissions.Books.Edit, L("Permission:Edit"));
        bookPermission.AddChild(AbpSuitePermissions.Books.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpSuiteResource>(name);
    }
}