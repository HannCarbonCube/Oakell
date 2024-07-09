using Volo.Abp.Modularity;

namespace Oakell;

public abstract class OakellApplicationTestBase<TStartupModule> : OakellTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
