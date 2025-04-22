namespace ApiAuth.Application.Features.Webhook.Integration.Queries.GetAccessToken;

public sealed record GetTokenWebhookQueryResponse(string AccessToken, string TokenType, int ExpiresIn);