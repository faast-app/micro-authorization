using ApiAuth.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace ApiAuth.OptionsSetup;

public class AuthOptionsSetup : IConfigureOptions<AuthOptions>
{
    private const string SectionName = "Auth";
    private readonly IConfiguration _configuration;

    public AuthOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void Configure(AuthOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}