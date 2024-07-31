using Microsoft.Extensions.Options;

namespace ApiAuth.Persistence.Options;

public class IntegracionDbOptionsSetup : IConfigureOptions<IntegracionDbOptions>
{
    private readonly IConfiguration _configuration;

    public IntegracionDbOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(IntegracionDbOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString("IntegracionDbConnection")!;
    }
}