using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AbpSuite;

public class AbpSuiteWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<AbpSuiteWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
