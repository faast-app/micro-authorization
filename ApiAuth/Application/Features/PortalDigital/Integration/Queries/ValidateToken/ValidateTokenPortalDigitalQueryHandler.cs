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
        var secretKey = _appSettings.Integration.PortalDigital.SecretKey;
        var jwtToken = request.AccessToken;

        var (isValid, tokenValidationResult, message) = await ValidateJwt(jwtToken, secretKey);
        if (!isValid) {
            return Result.Failure<ValidateTokenPortalDigitalQueryResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                message
                ));
        }

        var numeroDocumentoCliente = string.Empty;
        var nombreCliente = string.Empty;
        var tipoOferta = string.Empty;
        if (tokenValidationResult != null) {
            var claims = tokenValidationResult?.Claims.ToDictionary(c => c.Key, c => c.Value);
            numeroDocumentoCliente = claims != null && claims.ContainsKey("nroDoctoConsulta") ? claims["nroDoctoConsulta"].ToString() : string.Empty;
            nombreCliente = claims != null && claims.ContainsKey("nombreCliente") ? claims["nombreCliente"].ToString() : string.Empty;
            tipoOferta = claims != null && claims.ContainsKey("tipoOferta") ? claims["tipoOferta"].ToString() : string.Empty;
        }
        return Result.Success(new ValidateTokenPortalDigitalQueryResponse(true, numeroDocumentoCliente ?? "", nombreCliente ?? "", tipoOferta ?? ""));
    }

    private async Task<(bool isValid, TokenValidationResult? tokenValidationResult, string message)> ValidateJwt(string jwtToken, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (!tokenHandler.CanReadToken(jwtToken))
            return (false, null, "El token JWT no tiene un formato válido.");
        
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Constants.Integration.PortalDigital.Issuer,
            ValidAudience = Constants.Integration.PortalDigital.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero // Evita la tolerancia en el tiempo de expiración
        };
        var tokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(jwtToken, parameters);
        return (tokenValidationResult.IsValid, tokenValidationResult, string.Empty);
    }
}