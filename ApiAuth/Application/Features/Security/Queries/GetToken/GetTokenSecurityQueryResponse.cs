namespace ApiAuth.Application.Features.Security.Queries.GetToken;

public sealed record GetTokenSecurityQueryResponse(string AccessToken, string TokenType, int ExpiresIn);