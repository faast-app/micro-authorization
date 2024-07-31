using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.Auth.Queries.GetAccessToken;

public record GetAccessTokenQuery
(
    string ClientId,
    string GrantType,
    string ClientAssertionType,
    string ClientAssertion,
    string UserName,
    string Password
) : IQuery<GetAccessTokenResponse>;