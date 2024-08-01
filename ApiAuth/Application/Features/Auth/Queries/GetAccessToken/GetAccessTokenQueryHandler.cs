using ApiAuth.Application.Abstractions.Messaging;
using ApiAuth.Domain.Repositories;
using ApiAuth.Domain.Shared;
using ApiAuth.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiAuth.Application.Features.Auth.Queries.GetAccessToken;

internal sealed class GetAccessTokenQueryHandler: IQueryHandler<GetAccessTokenQuery, GetAccessTokenResponse>
{
    private readonly AuthOptions _options;
    private readonly IAuthRepository _authRepository;
    public GetAccessTokenQueryHandler
    (
        IOptions<AuthOptions> options,
        IAuthRepository authRepository
    )
    {
        _options = options.Value;
        _authRepository = authRepository;
    }

    public async Task<Result<GetAccessTokenResponse>> Handle
    (
        GetAccessTokenQuery request,
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
            return Result.Failure<GetAccessTokenResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                "Credenciales inválidas"
                ));
        }

        var jwtToken = request.ClientAssertion;
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(jwtToken);

        var audiences = _options.Audience.Split(',');
        var issuer = _options.Issuer;

        if (!jwtSecurityToken.Audiences.Any(audience => audiences.Contains(audience)))
        {
            return Result.Failure<GetAccessTokenResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                "Credenciales inválidas"
                ));
        }

        if (
            !(jwtSecurityToken.Subject == request.ClientId
            &&
            jwtSecurityToken.Issuer == issuer)
            )
        {
            return Result.Failure<GetAccessTokenResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                "Credenciales inválidas"
                ));
        }

        if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
        {
            return Result.Failure<GetAccessTokenResponse>(new Error(
                StatusCodes.Status400BadRequest.ToString(),
                "Credenciales inválidas"
                ));
        }

        var kid = jwtSecurityToken.Header.Kid;

        var jsonWebKeySet = GetJwksFromJson();
        var jsonWebKey = jsonWebKeySet.Keys.Where(w => w.Kid == kid).FirstOrDefault();

        if (
            jsonWebKey == null
            ||
            jsonWebKey.Kty != "RSA"
            ||
            jsonWebKey.Alg != "RS256"
            )
        {
            return Result.Failure<GetAccessTokenResponse>(new Error(
                            StatusCodes.Status400BadRequest.ToString(),
                            "Credenciales inválidas"
                            ));
        }

        //var rsa = ConvertJwkToRsa(jsonWebKey);

        var result = new GetAccessTokenResponse(GenerateAccessToken(request.ClientId), "Bearer", 299);
        return Result.Success(result);
    }

    private static JsonWebKeySet GetJwksFromJson()
    {
        var jsonString = File.ReadAllText(@"./cert/jwks.json"); // Leer el archivo JSON
        return JsonWebKeySet.Create(jsonString); // Deserializar JSON en JsonWebKeySet
    }
    private static RSACryptoServiceProvider ConvertJwkToRsa(JsonWebKey jwk)
    {
        // Extraer el módulo y el exponente de la clave JWK
        byte[] modulus = Base64UrlDecode(jwk.N);
        byte[] exponent = Base64UrlDecode(jwk.E);

        // Crear un objeto RSACryptoServiceProvider
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        // Importar la clave RSA
        rsa.ImportParameters(new RSAParameters
        {
            Modulus = modulus,
            Exponent = exponent
        });

        return rsa;
    }
    public static byte[] Base64UrlDecode(string input)
    {
        // Reemplazar caracteres
        input = input.Replace('-', '+').Replace('_', '/');

        // Agregar relleno
        switch (input.Length % 4)
        {
            case 0: break;
            case 2: input += "=="; break;
            case 3: input += "="; break;
            default: throw new ArgumentException("Invalid Base64Url string");
        }

        // Decodificar la cadena
        return Convert.FromBase64String(input);
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
            _options.Issuer,
            _options.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}