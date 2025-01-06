using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.Cavali.Integration.Queries.GetAccessToken;

public record GetTokenCavaliQuery
(
    string ClientId,
    string GrantType,
    string ClientAssertionType,
    string UserName,
    string Password
) : IQuery<GetTokenCavaliQueryResponse>;