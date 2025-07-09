using ApiAuth.Application.Features.Webhook.Integration.Queries.GetAccessToken;
using ApiAuth.Application.Features.Webhook.Integration.Queries.ValidateToken;
using Carter;
using MediatR;

namespace ApiAuth.Presentation;

public class IntegrationWebhookModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "api/v1/integration/webhook/token",
            async (
                GetTokenWebhookQuery request,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new GetTokenWebhookQuery(request.ClientId, request.GrantType, request.ClientAssertionType, request.UserName, request.Password);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);

            });

        app.MapGet(
            "api/v1/integration/webhook/token/validate/{token}",
            async (
                string token,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new ValidateTokenWebhookQuery(token);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);

            });

    }
}