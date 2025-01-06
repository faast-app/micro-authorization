using ApiAuth.Application.Abstractions.Messaging;
using ApiAuth.Domain.Repositories;
using ApiAuth.Domain.Shared;
using ApiAuth.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAuth.Application.Features.Cavali.Integration.Queries.GetAccessToken;

internal sealed class GetTokenCavaliQueryHandler: IQueryHandler<GetTokenCavaliQuery, GetTokenCavaliQueryResponse>
{
    private readonly AuthOptions _options;
    private readonly IAuthRepository _authRepository;
    public GetTokenCavaliQueryHandler
    (
        IOptions<AuthOptions> options,
        IAuthRepository authRepository
    )
    {
        _options = options.Value;
        _authRepository = authRepository;
    }

    public async Task<Result<GetTokenCavaliQueryResponse>> Handle
    (
        GetTokenCavaliQuery request,
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
            return Result.Failure<GetTokenCavaliQueryResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                "Credenciales inválidas"
                ));
        }

        var result = new GetTokenCavaliQueryResponse(GenerateAccessToken(request.ClientId), "Bearer", 299);
        return Result.Success(result);
    }

    private string GenerateAccessToken(string clientId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, clientId)
        };
        
        var token = new JwtSecurityToken
        (
            "API_AUTH",
            "CAVALI",
            claims,
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}