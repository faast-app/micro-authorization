using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ApiAuth.Application.Abstractions.Messaging;
using ApiAuth.Application.Features.Webhook.Integration.Queries.ValidateToken;
using ApiAuth.Common;
using ApiAuth.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuth.Application.Features.Auth.Queries.ValidateToken;

internal sealed class ValidateTokenWebhookQueryHandler: IQueryHandler<ValidateTokenWebhookQuery, ValidateTokenWebhookQueryResponse>
{
    private readonly AppSettings _appSettings;
    public ValidateTokenWebhookQueryHandler
    (
        IOptions<AppSettings> appSettings
    )
    {
        _appSettings = appSettings.Value;
    }

    public async Task<Result<ValidateTokenWebhookQueryResponse>> Handle
    (
        ValidateTokenWebhookQuery request,
        CancellationToken cancellationToken
    )
    {
        var secretKey = _appSettings.Integration.Webhook.SecretKey;
        var jwtToken = request.AccessToken;

        var (isValid, tokenValidationResult, message) = await ValidateJwt(jwtToken, secretKey);
        if (!isValid) {
            return Result.Failure<ValidateTokenWebhookQueryResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                message
                ));
        }

        return Result.Success(new ValidateTokenWebhookQueryResponse(true));
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
            ValidIssuer = Constants.Integration.Webhook.Issuer,
            ValidAudience = Constants.Integration.Webhook.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero // Evita la tolerancia en el tiempo de expiración
        };
        var tokenValidationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(jwtToken, parameters);
        return (tokenValidationResult.IsValid, tokenValidationResult, string.Empty);
    }
}