using ApiAuth.Application.Abstractions.Messaging;
using ApiAuth.Common;
using ApiAuth.Domain.Repositories;
using ApiAuth.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAuth.Application.Features.Security.Queries.GetToken;

internal sealed class GetTokenSecurityQueryHandler: IQueryHandler<GetTokenSecurityQuery, GetTokenSecurityQueryResponse>
{
    private readonly AppSettings _appSettings;
    private readonly IAuthRepository _authRepository;
    public GetTokenSecurityQueryHandler
    (
        IOptions<AppSettings> appSettings,
        IAuthRepository authRepository
    )
    {
        _appSettings = appSettings.Value;
        _authRepository = authRepository;
    }

    public async Task<Result<GetTokenSecurityQueryResponse>> Handle
    (
        GetTokenSecurityQuery request,
        CancellationToken cancellationToken
    )
    {

        var authList = await _authRepository.GetAllAsync();
        if (!authList.Any
        (
            w => w.ClientId == request.ClientId
            &&
            w.UserName == request.UserName
            &&
            w.Password == request.Password
        ))
        {
            return Result.Failure<GetTokenSecurityQueryResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                "Credenciales inválidas"
                ));
        }

        var result = new GetTokenSecurityQueryResponse(GenerateAccessToken(request.ClientId), "Bearer", 299);
        return Result.Success(result);
    }

    private string GenerateAccessToken(string clientId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Integration.Security.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, clientId)
        };
        
        var token = new JwtSecurityToken
        (
            Constants.Integration.Security.Issuer,
            Constants.Integration.Security.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}