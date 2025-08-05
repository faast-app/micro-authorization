using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.Webhook.Integration.Queries.ValidateToken;

public record ValidateTokenWebhookQuery
(
    string AccessToken
) : IQuery<ValidateTokenWebhookQueryResponse>;