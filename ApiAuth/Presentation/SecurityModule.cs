using ApiAuth.Application.Features.Security.Queries.GetToken;
using ApiAuth.Application.Features.Security.Queries.ValidateToken;
using Carter;
using MediatR;

namespace ApiAuth.Presentation;

public class SecurityModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "api/v1/integration/security/token",
            async (
                GetTokenSecurityQuery request,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new GetTokenSecurityQuery(request.ClientId, request.GrantType, request.ClientAssertionType, request.UserName, request.Password);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);

            });

        app.MapGet(
            "api/v1/integration/security/token/validate/{token}",
            async (
                string token,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new ValidateTokenSecurityQuery(token);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);

            });

    }
}