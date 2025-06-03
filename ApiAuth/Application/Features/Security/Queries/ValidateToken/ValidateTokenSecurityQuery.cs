using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.Security.Queries.ValidateToken;

public record ValidateTokenSecurityQuery
(
    string AccessToken
) : IQuery<ValidateTokenSecurityQueryResponse>;