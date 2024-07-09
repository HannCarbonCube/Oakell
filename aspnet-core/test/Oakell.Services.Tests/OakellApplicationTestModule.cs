using Volo.Abp.Modularity;

namespace Oakell;

[DependsOn(
    typeof(OakellApplicationModule),
    typeof(OakellEntitiesTestModule)
)]
public class OakellApplicationTestModule : AbpModule
{

}
