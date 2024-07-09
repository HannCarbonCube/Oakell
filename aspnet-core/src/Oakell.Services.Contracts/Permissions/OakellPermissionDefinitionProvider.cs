using Oakell.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Oakell.Permissions;

public class OakellPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(OakellPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(OakellPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OakellResource>(name);
    }
}
