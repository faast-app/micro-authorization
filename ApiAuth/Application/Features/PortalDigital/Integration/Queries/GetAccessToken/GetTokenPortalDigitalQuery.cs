using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.PortalDigital.Integration.Queries.GetAccessToken;

public record GetTokenPortalDigitalQuery
(
    string ClientId,
    string GrantType,
    string ClientAssertionType,
    string UserName,
    string Password
) : IQuery<GetTokenPortalDigitalQueryResponse>;