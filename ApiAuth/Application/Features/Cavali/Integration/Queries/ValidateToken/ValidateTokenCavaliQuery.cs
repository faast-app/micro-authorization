using ApiAuth.Application.Abstractions.Messaging;

namespace ApiAuth.Application.Features.Cavali.Integration.Queries.ValidateToken;

public record ValidateTokenCavaliQuery
(
    string AccessToken
) : IQuery<ValidateTokenCavaliQueryResponse>;