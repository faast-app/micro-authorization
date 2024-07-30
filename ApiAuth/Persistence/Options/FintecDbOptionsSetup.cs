using Microsoft.Extensions.Options;

namespace ApiAuth.Persistence.Options;

public class FintecDbOptionsSetup : IConfigureOptions<FintecDbOptions>
{
    private readonly IConfiguration _configuration;

    public FintecDbOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(FintecDbOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString("FintecDbConnection")!;
    }
}