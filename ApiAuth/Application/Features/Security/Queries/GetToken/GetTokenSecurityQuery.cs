using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.Security.Queries.GetToken;

public record GetTokenSecurityQuery
(
    string ClientId,
    string GrantType,
    string ClientAssertionType,
    string UserName,
    string Password
) : IQuery<GetTokenSecurityQueryResponse>;