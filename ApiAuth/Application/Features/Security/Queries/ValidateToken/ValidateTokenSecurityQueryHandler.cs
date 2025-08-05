using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApiAuth.Application.Abstractions.Messaging;
using ApiAuth.Common;
using ApiAuth.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuth.Application.Features.Security.Queries.ValidateToken;

internal sealed class ValidateTokenSecurityQueryHandler: IQueryHandler<ValidateTokenSecurityQuery, ValidateTokenSecurityQueryResponse>
{
    private readonly AppSettings _appSettings;
    public ValidateTokenSecurityQueryHandler
    (
        IOptions<AppSettings> appSettings
    )
    {
        _appSettings = appSettings.Value;
    }

    public async Task<Result<ValidateTokenSecurityQueryResponse>> Handle
    (
        ValidateTokenSecurityQuery request,
        CancellationToken cancellationToken
    )
    {
        // Configurar los parámetros de validación
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Constants.Integration.Security.Issuer,
            ValidAudience = Constants.Integration.Security.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Integration.Security.SecretKey)),
            ClockSkew = TimeSpan.Zero // Evita la tolerancia en el tiempo de expiración
        };

        var tokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(request.AccessToken, parameters);
        return Result.Success(new ValidateTokenSecurityQueryResponse(tokenValidationResult.IsValid));
    }
}