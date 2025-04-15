using ApiAuth.Application.Features.PortalDigital.Integration.Queries.GetAccessToken;
using ApiAuth.Application.Features.PortalDigital.Integration.Queries.ValidateToken;
using Carter;
using MediatR;

namespace ApiAuth.Presentation;

public class IntegrationPortalDigitalModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "api/v1/integration/portalDigital/token",
            async (
                GetTokenPortalDigitalQuery request,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new GetTokenPortalDigitalQuery(request.ClientId, request.GrantType, request.ClientAssertionType, request.UserName, request.Password);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);
            });

        app.MapGet(
            "api/v1/integration/portalDigital/token/validate/{token}",
            async (
                string token,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new ValidateTokenPortalDigitalQuery(token);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);
            });

    }
}