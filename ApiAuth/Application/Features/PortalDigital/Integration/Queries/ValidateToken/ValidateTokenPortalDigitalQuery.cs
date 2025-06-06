using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.PortalDigital.Integration.Queries.ValidateToken;

public record ValidateTokenPortalDigitalQuery
(
    string AccessToken
) : IQuery<ValidateTokenPortalDigitalQueryResponse>;