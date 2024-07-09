using Volo.Abp.Modularity;

namespace Oakell;

[DependsOn(
    typeof(OakellEntitiesModule),
    typeof(OakellTestBaseModule)
)]
public class OakellEntitiesTestModule : AbpModule
{

}
