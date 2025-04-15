namespace ApiAuth.Application.Features.Cavali.Integration.Queries.GetAccessToken;

public sealed record GetTokenCavaliQueryResponse(string AccessToken, string TokenType, int ExpiresIn);