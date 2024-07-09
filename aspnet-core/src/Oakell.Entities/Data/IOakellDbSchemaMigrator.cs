using System.Threading.Tasks;

namespace Oakell.Data;

public interface IOakellDbSchemaMigrator
{
    Task MigrateAsync();
}
