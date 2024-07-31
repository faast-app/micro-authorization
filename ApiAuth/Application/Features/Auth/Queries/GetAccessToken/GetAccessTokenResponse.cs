namespace ApiAuth.Application.Features.Auth.Queries.GetAccessToken;

public sealed record GetAccessTokenResponse(string AccessToken, string TokenType, int ExpiresIn);