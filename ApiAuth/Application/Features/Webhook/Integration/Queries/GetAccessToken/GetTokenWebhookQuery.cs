using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.Webhook.Integration.Queries.GetAccessToken;

public record GetTokenWebhookQuery
(
    string ClientId,
    string GrantType,
    string ClientAssertionType,
    string UserName,
    string Password
) : IQuery<GetTokenWebhookQueryResponse>;