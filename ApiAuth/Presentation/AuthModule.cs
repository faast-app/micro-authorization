using ApiAuth.Application.Features.Auth.Queries.GetAccessToken;
using Carter;
using MediatR;

namespace ApiAuth.Presentation;

public class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "api/v1/auth/token",
            async (
                GetAccessTokenQuery request,
                CancellationToken cancellationToken,
                ISender sender
                ) =>
            {
                var requestSend = new GetAccessTokenQuery(request.ClientId, request.GrantType, request.ClientAssertionType, request.ClientAssertion, request.UserName, request.Password);
                //var requestSend = new GetAccessTokenQuery(ClientId, GrantType, ClientAssertionType, ClientAssertion, UserName, Password);
                var result = await sender.Send(requestSend, cancellationToken);
                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result);

            });
    }
}