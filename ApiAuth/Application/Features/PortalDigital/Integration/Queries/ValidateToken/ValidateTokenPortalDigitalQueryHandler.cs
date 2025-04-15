using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApiAuth.Application.Abstractions.Messaging;
using ApiAuth.Application.Features.PortalDigital.Integration.Queries.ValidateToken;
using ApiAuth.Common;
using ApiAuth.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuth.Application.Features.Auth.Queries.ValidateToken;

internal sealed class ValidateTokenPortalDigitalQueryHandler: IQueryHandler<ValidateTokenPortalDigitalQuery, ValidateTokenPortalDigitalQueryResponse>
{
    private readonly AppSettings _appSettings;
    public ValidateTokenPortalDigitalQueryHandler
    (
        IOptions<AppSettings> appSettings
    )
    {
        _appSettings = appSettings.Value;
    }

    public async Task<Result<ValidateTokenPortalDigitalQueryResponse>> Handle
    (
        ValidateTokenPortalDigitalQuery request,
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
            ValidIssuer = Constants.Integration.PortalDigital.Issuer,
            ValidAudience = Constants.Integration.PortalDigital.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Integration.PortalDigital.SecretKey)),
            ClockSkew = TimeSpan.Zero // Evita la tolerancia en el tiempo de expiración
        };

        var tokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(request.AccessToken, parameters);
        return Result.Success(new ValidateTokenPortalDigitalQueryResponse(tokenValidationResult.IsValid));
    }
}