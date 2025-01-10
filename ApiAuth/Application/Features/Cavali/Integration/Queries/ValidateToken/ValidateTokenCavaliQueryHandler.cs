using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApiAuth.Application.Abstractions.Messaging;
using ApiAuth.Application.Features.Cavali.Integration.Queries.ValidateToken;
using ApiAuth.Common;
using ApiAuth.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuth.Application.Features.Auth.Queries.ValidateToken;

internal sealed class ValidateTokenCavaliQueryHandler: IQueryHandler<ValidateTokenCavaliQuery, ValidateTokenCavaliQueryResponse>
{
    private readonly AppSettings _appSettings;
    public ValidateTokenCavaliQueryHandler
    (
        IOptions<AppSettings> appSettings
    )
    {
        _appSettings = appSettings.Value;
    }

    public async Task<Result<ValidateTokenCavaliQueryResponse>> Handle
    (
        ValidateTokenCavaliQuery request,
        CancellationToken cancellationToken
    )
    {
        string token = request.AccessToken;

        // Configurar los parámetros de validación
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Constants.Integration.Cavali.Issuer,
            ValidAudience = Constants.Integration.Cavali.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Integration.Cavali.SecretKey)),
            ClockSkew = TimeSpan.Zero // Evita la tolerancia en el tiempo de expiración
        };

        try
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out SecurityToken securityToken);

            // var jwtToken = securityToken;
            // var tokenHandler = new JwtSecurityTokenHandler();
            // var jwtSecurityToken = tokenHandler.ReadJwtToken(jwtToken);

            return Result.Success(new ValidateTokenCavaliQueryResponse(true));
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine("El token no es válido: " + ex.Message);
            return Result.Failure<ValidateTokenCavaliQueryResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                "Token inválido"
                ));
        }
    }
}