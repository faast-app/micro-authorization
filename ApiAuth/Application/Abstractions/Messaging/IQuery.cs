using ApiAuth.Domain.Shared;
using MediatR;

namespace ApiAuth.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}