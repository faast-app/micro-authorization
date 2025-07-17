using ApiAuth.Application.Features.Cavali.Integration.Queries.GetAccessToken;
using ApiAuth.Application.Features.Cavali.Integration.Queries.ValidateToken;
using Carter;
using MediatR;

namespace ApiAuth.Presentation;

public class IntegrationCavaliModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "api/v1/integration/cavali/token",
            async (
                GetTokenCavaliQuery request,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new GetTokenCavaliQuery(request.ClientId, request.GrantType, request.ClientAssertionType, request.UserName, request.Password);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);

            });

        app.MapGet(
            "api/v1/integration/cavali/token/validate/{token}",
            async (
                string token,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new ValidateTokenCavaliQuery(token);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);

            });

    }
}