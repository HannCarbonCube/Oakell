using Volo.Abp.Modularity;

namespace Oakell;

/* Inherit from this class for your domain layer tests. */
public abstract class OakellEntitiesTestBase<TStartupModule> : OakellTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
