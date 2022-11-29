using System.Threading.Tasks;

namespace AbpSuite.Data;

public interface IAbpSuiteDbSchemaMigrator
{
    Task MigrateAsync();
}
