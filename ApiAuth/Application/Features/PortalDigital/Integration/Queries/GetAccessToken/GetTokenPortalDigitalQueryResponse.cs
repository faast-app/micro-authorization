namespace ApiAuth.Application.Features.PortalDigital.Integration.Queries.GetAccessToken;

public sealed record GetTokenPortalDigitalQueryResponse(string AccessToken, string TokenType, int ExpiresIn);